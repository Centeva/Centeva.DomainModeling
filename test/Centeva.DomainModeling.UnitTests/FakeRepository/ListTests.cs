using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;
public class ListTests
{
    private readonly FakeRepository<Person, Guid>  _repository = new();

    [Fact]
    public async Task WithoutSpec_ReturnsAllEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.ListAsync(TestContext.Current.CancellationToken);

        result.Count.Should().Be(3);
    }

    [Fact]
    public async Task WhenNoEntities_ReturnsEmptyList()
    {
        var result = await _repository.ListAsync(TestContext.Current.CancellationToken);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task WithSpec_ReturnsMatchingEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.ListAsync(new PersonByNameSpec("Doe"), TestContext.Current.CancellationToken);

        result.Count.Should().Be(2);
    }

    [Fact]
    public async Task WithoutSpecUsingProjection_ReturnsAllProjectedEntities()
    {
        var entities = PersonSeed.Get();
        await _repository.AddRangeAsync(entities, TestContext.Current.CancellationToken);

        var result = await _repository.ListAsync(x => x.Name, TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(entities.Select(x => x.Name).ToList());
    }

    [Fact]
    public async Task WithSpecUsingProjection_ReturnsMatchingProjectedEntities()
    {
        var entities = PersonSeed.Get();
        await _repository.AddRangeAsync(entities, TestContext.Current.CancellationToken);

        var result = await _repository.ListAsync(new PersonByNameSpec("Doe"), x => x.Name, TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(entities.Where(x => x.Name.Contains("Doe")).Select(x => x.Name).ToList());
    }

    [Fact]
    public async Task WithSelectSpec_ReturnsMatchingProjectedEntities()
    {
        var entities = PersonSeed.Get();
        await _repository.AddRangeAsync(entities, TestContext.Current.CancellationToken);

        var result = await _repository.ListAsync(new PersonNameSpec(PersonSeed.ValidPersonId), TestContext.Current.CancellationToken);

        result.Count.Should().Be(1);
        result.Should().Contain(PersonSeed.ValidPersonName);
    }
}

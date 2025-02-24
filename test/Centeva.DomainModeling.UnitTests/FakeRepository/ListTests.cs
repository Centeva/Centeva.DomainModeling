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
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.ListAsync();

        result.Count.ShouldBe(3);
    }

    [Fact]
    public async Task WhenNoEntities_ReturnsEmptyList()
    {
        var result = await _repository.ListAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task WithSpec_ReturnsMatchingEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.ListAsync(new PersonByNameSpec("Doe"));

        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task WithoutSpecUsingProjection_ReturnsAllProjectedEntities()
    {
        var entities = PersonSeed.Get();
        await _repository.AddRangeAsync(entities);

        var result = await _repository.ListAsync(x => x.Name);

        result.ShouldBeEquivalentTo(entities.Select(x => x.Name).ToList());
    }

    [Fact]
    public async Task WithSpecUsingProjection_ReturnsMatchingProjectedEntities()
    {
        var entities = PersonSeed.Get();
        await _repository.AddRangeAsync(entities);

        var result = await _repository.ListAsync(new PersonByNameSpec("Doe"), x => x.Name);

        result.ShouldBeEquivalentTo(entities.Where(x => x.Name.Contains("Doe")).Select(x => x.Name).ToList());
    }

    [Fact]
    public async Task WithSelectSpec_ReturnsMatchingProjectedEntities()
    {
        var entities = PersonSeed.Get();
        await _repository.AddRangeAsync(entities);

        var result = await _repository.ListAsync(new PersonNameSpec(PersonSeed.ValidPersonId));

        result.Count.ShouldBe(1);
        result.ShouldContain(PersonSeed.ValidPersonName);
    }
}

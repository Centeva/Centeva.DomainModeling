using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;
public class AggregateTests
{
    private readonly FakeRepository<Person, Guid>  _repository = new();

    [Fact]
    public async Task AnyAsync_ReturnsTrueWhenEntitiesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.AnyAsync(TestContext.Current.CancellationToken);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AnyAsync_ReturnsFalseWithNoEntities()
    {
        var result = await _repository.AnyAsync(TestContext.Current.CancellationToken);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsTrueWhenMatchesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.AnyAsync(new PersonByNameSpec(PersonSeed.ValidPersonName), TestContext.Current.CancellationToken);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsFalseWhenNoMatchesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.AnyAsync(new PersonByNameSpec("bad"), TestContext.Current.CancellationToken);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task CountAsync_CountsEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.CountAsync(TestContext.Current.CancellationToken);

        result.ShouldBe(3);
    }

    [Fact]
    public async Task CountAsync_WithSpec_CountsMatchingEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.CountAsync(new PersonByNameSpec(PersonSeed.ValidPersonName), TestContext.Current.CancellationToken);

        result.ShouldBe(1);
    }
}

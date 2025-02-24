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
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.AnyAsync();

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AnyAsync_ReturnsFalseWithNoEntities()
    {
        var result = await _repository.AnyAsync();

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsTrueWhenMatchesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.AnyAsync(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsFalseWhenNoMatchesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.AnyAsync(new PersonByNameSpec("bad"));

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task CountAsync_CountsEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.CountAsync();

        result.ShouldBe(3);
    }

    [Fact]
    public async Task CountAsync_WithSpec_CountsMatchingEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.CountAsync(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.ShouldBe(1);
    }
}

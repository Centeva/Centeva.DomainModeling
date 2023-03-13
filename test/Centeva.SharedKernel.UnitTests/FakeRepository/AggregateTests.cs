using Centeva.SharedKernel.Testing;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;
using Centeva.SharedKernel.UnitTests.Fixtures.Seeds;
using Centeva.SharedKernel.UnitTests.Fixtures.Specs;

namespace Centeva.SharedKernel.UnitTests.FakeRepository;
public class AggregateTests
{
    private readonly FakeRepository<Person, Guid>  _repository = new();

    [Fact]
    public async Task AnyAsync_ReturnsTrueWhenEntitiesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.AnyAsync();

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AnyAsync_ReturnsFalseWithNoEntities()
    {
        var result = await _repository.AnyAsync();

        result.Should().BeFalse();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsTrueWhenMatchesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.AnyAsync(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsFalseWhenNoMatchesExist()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.AnyAsync(new PersonByNameSpec("bad"));

        result.Should().BeFalse();
    }

    [Fact]
    public async Task CountAsync_CountsEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.CountAsync();

        result.Should().Be(3);
    }

    [Fact]
    public async Task CountAsync_WithSpec_CountsMatchingEntities()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.CountAsync(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().Be(1);
    }
}

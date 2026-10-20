using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class AggregateTests : IntegrationTestBase
{
    public AggregateTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task AnyAsync_ReturnsTrueWhenEntitiesExist()
    {
        var result = await _personRepository.AnyAsync(TestContext.Current.CancellationToken);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AnyAsync_ReturnsFalseWithNoEntities()
    {
        await _dbContext.Addresses.ExecuteDeleteAsync(TestContext.Current.CancellationToken);
        await _dbContext.People.ExecuteDeleteAsync(TestContext.Current.CancellationToken);

        var result = await _personRepository.AnyAsync(TestContext.Current.CancellationToken);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsTrueWhenMatchesExist()
    {
        var result = await _personRepository.AnyAsync(new PersonByNameSpec(PersonSeed.ValidPersonName), TestContext.Current.CancellationToken);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AnyAsync_WithSpec_ReturnsFalseWhenNoMatchesExist()
    {
        var result = await _personRepository.AnyAsync(new PersonByNameSpec("bad"), TestContext.Current.CancellationToken);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task CountAsync_CountsEntities()
    {
        var result = await _personRepository.CountAsync(TestContext.Current.CancellationToken);

        result.Should().Be(3);
    }

    [Fact]
    public async Task CountAsync_WithSpec_CountsMatchingEntities()
    {
        var result = await _personRepository.CountAsync(new PersonByNameSpec("Doe"), TestContext.Current.CancellationToken);

        result.Should().Be(2);
    }
}

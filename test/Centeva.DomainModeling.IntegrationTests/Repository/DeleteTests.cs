using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class DeleteTests : IntegrationTestBase
{
    public DeleteTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task DeleteAsync_RemovesEntity()
    {
        var person = await _dbContext.People.SingleAsync(x => x.Id == PersonSeed.ValidPersonId, TestContext.Current.CancellationToken);

        await _personRepository.DeleteAsync(person, TestContext.Current.CancellationToken);

        _dbContext.ChangeTracker.Clear();
        var remaining = await _dbContext.People.ToListAsync(TestContext.Current.CancellationToken);
        remaining.Should().HaveCount(2);
        remaining.Should().NotContain(x => x.Id == PersonSeed.ValidPersonId);
    }

    [Fact]
    public async Task DeleteRangeAsync_RemovesEntities()
    {
        var ids = new[] { PersonSeed.ValidPersonId, PersonSeed.ValidPersonId2 };
        var people = await _dbContext.People.Where(x => ids.Contains(x.Id)).ToListAsync(TestContext.Current.CancellationToken);

        await _personRepository.DeleteRangeAsync(people, TestContext.Current.CancellationToken);

        _dbContext.ChangeTracker.Clear();
        var remaining = await _dbContext.People.ToListAsync(TestContext.Current.CancellationToken);
        remaining.Should().ContainSingle(x => x.Id == PersonSeed.ValidPersonId3);
    }
}

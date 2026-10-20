using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class UpdateTests : IntegrationTestBase
{
    public UpdateTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task UpdateAsync_ModifiesMatchingEntity()
    {
        var update = new Person(PersonSeed.ValidPersonId, "New name");

        await _personRepository.UpdateAsync(update, TestContext.Current.CancellationToken);

        _dbContext.ChangeTracker.Clear();
        var saved = await _dbContext.People.SingleAsync(x => x.Id == PersonSeed.ValidPersonId, TestContext.Current.CancellationToken);
        saved.Name.Should().Be("New name");
        (await _dbContext.People.CountAsync(TestContext.Current.CancellationToken)).Should().Be(3);
    }

    [Fact]
    public async Task UpdateRangeAsync_ModifiesMatchingEntities()
    {
        var update = new List<Person>
        {
            new Person(PersonSeed.ValidPersonId, "New name"),
            new Person(PersonSeed.ValidPersonId2, "New name 2")
        };

        await _personRepository.UpdateRangeAsync(update, TestContext.Current.CancellationToken);

        _dbContext.ChangeTracker.Clear();
        var saved = await _dbContext.People.ToListAsync(TestContext.Current.CancellationToken);
        saved.Should().HaveCount(3);
        saved.Should().Contain(x => x.Id == PersonSeed.ValidPersonId && x.Name == "New name");
        saved.Should().Contain(x => x.Id == PersonSeed.ValidPersonId2 && x.Name == "New name 2");
    }
}

using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class SaveChangesTests : IntegrationTestBase
{
    public SaveChangesTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task SaveChangesAsync_PersistsTrackedChanges()
    {
        var person = new Person(Guid.NewGuid(), "Tracked Person");
        _dbContext.People.Add(person);

        var result = await _personRepository.SaveChangesAsync(TestContext.Current.CancellationToken);

        result.Should().Be(1);
        _dbContext.ChangeTracker.Clear();
        (await _dbContext.People.AnyAsync(x => x.Id == person.Id, TestContext.Current.CancellationToken)).Should().BeTrue();
    }
}

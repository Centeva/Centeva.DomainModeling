using Centeva.DomainModeling.EFCore;
using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.IntegrationTests;

public class DbContextExtensionsTests : IntegrationTestBase
{
    public DbContextExtensionsTests(SharedDatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public void GetEntitiesWithEvents()
    {
        var person = new Person(Guid.NewGuid(), "Test");
        var address = new Address();

        _dbContext.People.Add(person);
        _dbContext.Addresses.Add(address);

        person.DomainEvents.ShouldHaveSingleItem();
        _dbContext.GetEntitiesWithEvents().ShouldContain(person);
    }
}

using Centeva.SharedKernel.EFCore;
using Centeva.SharedKernel.IntegrationTests.Fixtures;
using Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;
using FluentAssertions;

namespace Centeva.SharedKernel.IntegrationTests;

public class DbContextExtensionsTests : IntegrationTestBase
{
    public DbContextExtensionsTests(SharedDatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public void GetEntitiesWithEvents()
    {
        var person = new Person("Test");
        var address = new Address();

        _dbContext.People.Add(person);
        _dbContext.Addresses.Add(address);

        person.DomainEvents.Should().ContainSingle();
        _dbContext.GetEntitiesWithEvents().Should().OnlyContain(x => x == person);
    }
}

using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class AddTests : IntegrationTestBase
{
    public AddTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task AddAsync_PersistsEntity()
    {
        var person = new Person(Guid.NewGuid(), "New Person");

        var result = await _personRepository.AddAsync(person, TestContext.Current.CancellationToken);

        result.Should().BeSameAs(person);
        _dbContext.ChangeTracker.Clear();
        var saved = await _dbContext.People.SingleOrDefaultAsync(x => x.Id == person.Id, TestContext.Current.CancellationToken);
        saved.Should().NotBeNull();
        saved!.Name.Should().Be("New Person");
    }

    [Fact]
    public async Task AddRangeAsync_PersistsEntities()
    {
        var people = new List<Person>
        {
            new Person(Guid.NewGuid(), "New Person 1"),
            new Person(Guid.NewGuid(), "New Person 2")
        };

        var result = await _personRepository.AddRangeAsync(people, TestContext.Current.CancellationToken);

        result.Should().BeEquivalentTo(people);
        _dbContext.ChangeTracker.Clear();
        var ids = people.Select(x => x.Id).ToList();
        var saved = await _dbContext.People.Where(x => ids.Contains(x.Id)).ToListAsync(TestContext.Current.CancellationToken);
        saved.Select(x => x.Name).Should().BeEquivalentTo("New Person 1", "New Person 2");
    }
}

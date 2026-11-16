using Centeva.DomainModeling.EFCore;
using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Moq;

namespace Centeva.DomainModeling.IntegrationTests;

public class InterceptorTests : IntegrationTestBase
{
    public InterceptorTests(SharedDatabaseFixture fixture) : base(fixture)
    {
        // The dispatcher mock is shared by every test in this class
        Mock.Get(_dispatcher).Invocations.Clear();
    }

    [Fact]
    public void Constructor_ThrowsWhenDispatcherIsNull()
    {
        var act = () => new DispatchDomainEventsInterceptor(null!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("domainEventDispatcher");
    }

    [Fact]
    public async Task PostSaveEventDispatchingInterceptor_DispatchesEvents()
    {
        var person = new Person(Guid.NewGuid(), "Test");

        _dbContext.People.Add(person);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        Mock.Get(_dispatcher)
            .Verify<Task>(
                x => x.DispatchAsync(
                    It.Is<IEnumerable<IDomainEvent>>(events =>
                        events.Count() == 1 && events.Single() is PersonCreatedEvent),
                    It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task PostSaveEventDispatchingInterceptor_ClearsEventsFromEntities()
    {
        var person = new Person(Guid.NewGuid(), "Test");

        _dbContext.People.Add(person);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        person.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public async Task PostSaveEventDispatchingInterceptor_DoesNotDispatchWhenNoEntitiesHaveEvents()
    {
        _dbContext.Addresses.Add(new Address { PersonId = PersonSeed.ValidPersonId3, Street = "1 New Street" });
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        Mock.Get(_dispatcher)
            .Verify<Task>(
                x => x.DispatchAsync(It.IsAny<IEnumerable<IDomainEvent>>(), It.IsAny<CancellationToken>()),
                Times.Never);
    }

    [Fact]
    public void PostSaveEventDispatchingInterceptor_DoesNotDispatchOnSynchronousSave()
    {
        var person = new Person(Guid.NewGuid(), "Test");

        _dbContext.People.Add(person);
        var result = _dbContext.SaveChanges();

        result.Should().Be(1);
        Mock.Get(_dispatcher)
            .Verify<Task>(
                x => x.DispatchAsync(It.IsAny<IEnumerable<IDomainEvent>>(), It.IsAny<CancellationToken>()),
                Times.Never);
    }
}

namespace Centeva.DomainModeling.UnitTests;

public class ObjectWithEventsTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyEvents()
    {
        var entity = new TestEntity();

        entity.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void RegisterDomainEvent_AddsDomainEventToEntity()
    {
        var entity = new TestEntity();

        entity.AddTestEvent();

        entity.DomainEvents.ShouldHaveSingleItem();
        entity.DomainEvents.ShouldAllBe(x => x is TestEvent);
    }

    [Fact]
    public void RegisterDomainEvent_CanAddMultipleEvents()
    {
        var entity = new TestEntity();

        entity.AddTestEvent();
        entity.AddTestEvent();
        entity.AddTestEvent();

        entity.DomainEvents.Count().ShouldBe(3);
        entity.DomainEvents.ShouldAllBe(x => x is TestEvent);
    }

    [Fact]
    public void DomainEvents_ReturnsReadOnlyCollection()
    {
        var entity = new TestEntity();
        entity.AddTestEvent();

        var events = entity.DomainEvents;

        // Should return a read-only collection
        events.ShouldNotBeNull();
        // Verify it's the IEnumerable abstraction
        events.GetType().Name.ShouldContain("ReadOnly");
    }

    private class TestEntity : ObjectWithEvents
    {
        public void AddTestEvent()
        {
            RegisterDomainEvent(new TestEvent());
        }
    }

    private class TestEvent : IDomainEvent
    {
        public DateTime DateOccurred { get; set; }
    }
}

namespace Centeva.DomainModeling.UnitTests;

public class ObjectWithEventsTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyEvents()
    {
        var entity = new TestEntity();

        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void RegisterDomainEvent_AddsDomainEventToEntity()
    {
        var entity = new TestEntity();

        entity.AddTestEvent();

        entity.DomainEvents.Should().HaveCount(1);
        entity.DomainEvents.Should().OnlyContain(x => x is TestEvent);
    }

    [Fact]
    public void RegisterDomainEvent_CanAddMultipleEvents()
    {
        var entity = new TestEntity();

        entity.AddTestEvent();
        entity.AddTestEvent();
        entity.AddTestEvent();

        entity.DomainEvents.Count().Should().Be(3);
        entity.DomainEvents.Should().OnlyContain(x => x is TestEvent);
    }

    [Fact]
    public void DomainEvents_ReturnsReadOnlyCollection()
    {
        var entity = new TestEntity();
        entity.AddTestEvent();

        var events = entity.DomainEvents;

        // Should return a read-only collection
        events.Should().NotBeNull();
        // Verify it's the IEnumerable abstraction
        events.GetType().Name.Should().Contain("ReadOnly");
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

namespace Centeva.DomainModeling.UnitTests;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyEvents()
    {
        var entity = new TestEntity();

        entity.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void Constructor_SetsDefaultId()
    {
        var entity = new TestEntity();

        entity.Id.ShouldBe(default);
    }

    [Fact]
    public void RegisterDomainEvent_AddsDomainEventToEntity()
    {
        var entity = new TestEntity();

        entity.AddTestEvent();

        entity.DomainEvents.ShouldHaveSingleItem();
        entity.DomainEvents.ShouldAllBe(x => x is TestEvent);
    }

    class TestEntity : BaseEntity
    {
        public void AddTestEvent()
        {
            RegisterDomainEvent(new TestEvent());
        }
    }

    class TestEvent : IDomainEvent
    {
    }
}

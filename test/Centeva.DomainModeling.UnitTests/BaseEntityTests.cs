namespace Centeva.DomainModeling.UnitTests;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyEvents()
    {
        var entity = new TestEntity();

        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_SetsDefaultId()
    {
        var entity = new TestEntity();

        entity.Id.Should().Be(default);
    }

    [Fact]
    public void RegisterDomainEvent_AddsDomainEventToEntity()
    {
        var entity = new TestEntity();

        entity.AddTestEvent();

        entity.DomainEvents.Should().HaveCount(1);
        entity.DomainEvents.Should().AllBeOfType<TestEvent>();
    }

    class TestEntity : BaseEntity
    {
        public void AddTestEvent()
        {
            RegisterDomainEvent(new TestEvent());
        }
    }

    class TestEvent : BaseDomainEvent
    {
    }
}

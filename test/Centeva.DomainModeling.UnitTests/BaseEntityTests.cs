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
    public void RegisterDomainEvent_AddsEvent()
    {
        var entity = new TestEntity();
        var ev = new TestEvent();
        entity.RegisterDomainEvent(ev);

        entity.DomainEvents.Should().ContainSingle(x => x == ev);
    }

    [Fact]
    public void RemoveDomainEvent_RemovesEvent()
    {
        var entity = new TestEntity();
        var ev = new TestEvent();

        entity.RegisterDomainEvent(ev);
        entity.RemoveDomainEvent(ev);

        entity.DomainEvents.Should().BeEmpty();
    }

    class TestEntity : BaseEntity
    {
    }

    class TestEvent : BaseDomainEvent
    {
    }
}

using Centeva.SharedKernel.UnitTests.Fixtures;

namespace Centeva.SharedKernel.UnitTests;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyEvents()
    {
        var entity = new TestEntity("test");

        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_ShouldSetDefaultId()
    {
        var entity = new TestEntity("test");

        entity.Id.Should().Be(default);
    }

    [Fact]
    public void RegisterDomainEvent_AddsEvent()
    {
        var entity = new TestEntity("test");
        entity.ChangeName("new");

        entity.DomainEvents.Should().NotBeEmpty();
    }
}

namespace Centeva.DomainModeling.UnitTests;

public class BaseEntityTests
{
    [Fact]
    public void Constructor_SetsDefaultId()
    {
        var entity = new TestEntity();

        entity.Id.Should().Be((int)default);
    }

    [Fact]
    public void Constructor_AllowsIdToBeSet()
    {
        var entity = new TestEntity { Id = 42 };

        entity.Id.Should().Be(42);
    }

    [Fact]
    public void Constructor_WithGuidId_SetsDefaultId()
    {
        var entity = new TestEntityWithGuidId();
        entity.Id.Should().Be(Guid.Empty);
    }

    [Fact]
    public void Constructor_WithGuidId_AllowsIdToBeSet()
    {
        var guid = Guid.NewGuid();
        var entity = new TestEntityWithGuidId { Id = guid };
        entity.Id.Should().Be(guid);
    }

    private class TestEntity : BaseEntity
    {
    }

    private class TestEntityWithGuidId : BaseEntity<Guid>
    {
    }
}

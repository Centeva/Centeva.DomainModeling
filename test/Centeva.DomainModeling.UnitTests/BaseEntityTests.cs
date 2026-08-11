namespace Centeva.DomainModeling.UnitTests;

public class BaseEntityTests
{
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

    [Fact]
    public void Constructor_WithStronglyTypedId_AllowsIdToBeSet()
    {
        var id = new CustomerId(Guid.NewGuid());
        var entity = new TestEntityWithStronglyTypedId { Id = id };

        entity.Id.Should().Be(id);
        entity.Id.Value.Should().Be(id.Value);
    }

    private class TestEntityWithGuidId : BaseEntity<Guid>
    {
    }

    private readonly record struct CustomerId(Guid Value);

    private class TestEntityWithStronglyTypedId : BaseEntity<CustomerId>
    {
    }
}

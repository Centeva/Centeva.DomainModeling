using FluentAssertions;
using Xunit;

namespace Centeva.SharedKernel.UnitTests;

public class BaseEntityTests
{
    public class TestEntity : BaseEntity<int>
    {

    }

    [Fact]
    public void Constructor_ShouldInitializeEmptyEvents()
    {
        var entity = new TestEntity();

        entity.Events.Should().BeEmpty();
    }
}

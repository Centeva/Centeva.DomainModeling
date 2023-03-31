namespace Centeva.DomainModeling.UnitTests;

public class BaseDomainEventTests
{
    public class TestEvent : BaseDomainEvent
    {
    }

    [Fact]
    public void Constructor_ShouldSetOccurredToCurrentTime()
    {
        var newEvent = new TestEvent();

        // Not really another way to test this
        newEvent.Occurred.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(100));
    }
}
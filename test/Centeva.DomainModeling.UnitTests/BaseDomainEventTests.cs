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
        newEvent.DateOccurred.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(100));
    }
}
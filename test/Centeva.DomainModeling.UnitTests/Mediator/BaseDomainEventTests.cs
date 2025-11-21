
namespace Centeva.DomainModeling.UnitTests.Mediator;

public class BaseDomainEventTests
{
    public class TestEvent : DomainModeling.Mediator.BaseDomainEvent
    {
    }

    [Fact]
    public void Constructor_ShouldSetOccurredToCurrentTime()
    {
        var newEvent = new TestEvent();

        // Not really another way to test this
        newEvent.DateOccurred.ShouldBe(DateTime.UtcNow, TimeSpan.FromMilliseconds(100));
    }
}
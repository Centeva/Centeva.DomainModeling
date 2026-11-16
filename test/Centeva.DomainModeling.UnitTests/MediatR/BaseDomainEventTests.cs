namespace Centeva.DomainModeling.UnitTests.MediatR;

public class BaseDomainEventTests
{
    public class TestEvent : DomainModeling.MediatR.BaseDomainEvent
    {
    }

    [Fact]
    public void Constructor_ShouldSetOccurredToCurrentTime()
    {
        var before = DateTime.UtcNow;
        var newEvent = new TestEvent();
        var after = DateTime.UtcNow;

        // Bounded by readings taken on either side, so a slow test runner can't cause a failure
        newEvent.DateOccurred.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
        newEvent.DateOccurred.Kind.Should().Be(DateTimeKind.Utc);
    }
}

namespace Centeva.SharedKernel.UnitTests;

public class BaseIntegrationEventTests
{
    public class TestIntegrationEvent : BaseIntegrationEvent
    {
        public override string EventType => nameof(TestIntegrationEvent);
    }

    [Fact]
    public void Constructor_ShouldSetOccurredToCurrentTime()
    {
        var newEvent = new TestIntegrationEvent();

        newEvent.Occurred.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(100));
    }
}

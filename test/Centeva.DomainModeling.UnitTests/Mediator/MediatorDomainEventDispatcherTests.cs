using Centeva.DomainModeling.Mediator;
using Mediator;

namespace Centeva.DomainModeling.UnitTests.Mediator;

public class MediatorDomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly MediatorDomainEventDispatcher _sut;

    public MediatorDomainEventDispatcherTests()
    {
        _sut = new MediatorDomainEventDispatcher(_publisher);
    }

    [Fact]
    public void Constructor_ThrowsWhenPublisherIsNull()
    {
        var act = () => new MediatorDomainEventDispatcher(null!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("publisher");
    }

    [Fact]
    public async Task DispatchAsync_PublishesEachEventInOrder()
    {
        var first = new TestEvent();
        var second = new TestEvent();
        var published = new List<object>();
        Mock.Get(_publisher)
            .Setup(x => x.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback<object, CancellationToken>((e, _) => published.Add(e));

        await _sut.DispatchAsync([first, second], TestContext.Current.CancellationToken);

        published.Should().Equal(first, second);
    }

    [Fact]
    public async Task DispatchAsync_PublishesNothingWhenNoEvents()
    {
        await _sut.DispatchAsync([], TestContext.Current.CancellationToken);

        Mock.Get(_publisher)
            .Verify(
                x => x.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()),
                Times.Never);
    }

    private sealed class TestEvent : INotification, IDomainEvent
    {
        public DateTime DateOccurred { get; } = DateTime.UtcNow;
    }
}

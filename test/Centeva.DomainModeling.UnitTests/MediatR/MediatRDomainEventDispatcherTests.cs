using Centeva.DomainModeling.MediatR;
using MediatR;

namespace Centeva.DomainModeling.UnitTests.MediatR;

public class MediatRDomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly MediatRDomainEventDispatcher _sut;

    public MediatRDomainEventDispatcherTests()
    {
        _sut = new MediatRDomainEventDispatcher(_publisher);
    }

    [Fact]
    public void Constructor_ThrowsWhenPublisherIsNull()
    {
        var act = () => new MediatRDomainEventDispatcher(null!);

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

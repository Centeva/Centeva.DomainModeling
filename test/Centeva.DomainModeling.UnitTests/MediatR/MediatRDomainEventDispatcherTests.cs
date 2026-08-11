using Centeva.DomainModeling.MediatR;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using MediatR;

namespace Centeva.DomainModeling.UnitTests.MediatR;

public class MediatRDomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly MediatRDomainEventDispatcher _sut;
    private readonly Person _entity;

    public MediatRDomainEventDispatcherTests()
    {
        _sut = new MediatRDomainEventDispatcher(_publisher);

        _entity = new Person(Guid.NewGuid(), "Joe Test");
    }

    [Fact]
    public async Task DispatchAndClearEvents_DispatchesEvents()
    {
        await _sut.DispatchAndClearEvents([_entity], TestContext.Current.CancellationToken);

        Mock.Get(_publisher)
            .Verify(
                x => x.Publish(It.Is<object>(x => x is PersonCreatedEvent), It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task DispatchAndClearEvents_ClearsEvents()
    {
        await _sut.DispatchAndClearEvents([_entity], TestContext.Current.CancellationToken);

        _entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public async Task DispatchAndClearEvents_DispatchesAndClearsEventsFromCustomIHasDomainEventsImplementer()
    {
        var customEmitter = new CustomEventEmitter();
        customEmitter.RaiseTestEvent();

        await _sut.DispatchAndClearEvents([customEmitter], TestContext.Current.CancellationToken);

        Mock.Get(_publisher)
            .Verify(
                x => x.Publish(It.Is<object>(e => e is CustomEventEmitter.CustomTestEvent), It.IsAny<CancellationToken>()),
                Times.Once);
        customEmitter.DomainEvents.Should().BeEmpty();
    }

    // Verifies the dispatcher works with any IHasDomainEvents implementer, not just types
    // derived from ObjectWithEvents.
    private sealed class CustomEventEmitter : IHasDomainEvents
    {
        private readonly List<IDomainEvent> _events = [];

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events;

        public void ClearDomainEvents() => _events.Clear();

        public void RaiseTestEvent() => _events.Add(new CustomTestEvent());

        public sealed class CustomTestEvent : INotification, IDomainEvent
        {
            public DateTime DateOccurred { get; } = DateTime.UtcNow;
        }
    }
}

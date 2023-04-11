using Centeva.DomainModeling.Interfaces;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using MediatR;

namespace Centeva.DomainModeling.UnitTests;

public class DomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly DomainEventDispatcher _sut;
    private readonly Person _entity;

    public DomainEventDispatcherTests()
    {
        _sut = new DomainEventDispatcher(_publisher);

        _entity = new Person(Guid.NewGuid(), "Joe Test");
    }

    [Fact]
    public async Task DispatchAndClearEvents_DispatchesEvents()
    {
        await _sut.DispatchAndClearEvents(new List<IEntityWithEvents> {_entity});

        Mock.Get(_publisher)
            .Verify(
                x => x.Publish(It.Is<BaseDomainEvent>(ev => ev is PersonCreatedEvent), It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task DispatchAndClearEvents_ClearsEvents()
    {
        await _sut.DispatchAndClearEvents(new List<IEntityWithEvents> {_entity});

        _entity.DomainEvents.Should().BeEmpty();
    }
}

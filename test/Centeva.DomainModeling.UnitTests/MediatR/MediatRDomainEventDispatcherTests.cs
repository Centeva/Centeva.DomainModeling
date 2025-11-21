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
        await _sut.DispatchAndClearEvents([_entity]);

        Mock.Get(_publisher)
            .Verify(
                x => x.Publish(It.Is<object>(x => x is PersonCreatedEvent), It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task DispatchAndClearEvents_ClearsEvents()
    {
        await _sut.DispatchAndClearEvents([_entity]);

        _entity.DomainEvents.ShouldBeEmpty();
    }
}

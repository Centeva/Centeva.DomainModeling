using Centeva.DomainModeling.Mediator;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Mediator;

namespace Centeva.DomainModeling.UnitTests.Mediator;

public class MediatorDomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly MediatorDomainEventDispatcher _sut;
    private readonly Person _entity;

    public MediatorDomainEventDispatcherTests()
    {
        _sut = new MediatorDomainEventDispatcher(_publisher);

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

        _entity.DomainEvents.ShouldBeEmpty();
    }
}

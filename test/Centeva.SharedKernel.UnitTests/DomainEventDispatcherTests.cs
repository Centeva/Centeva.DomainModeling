using Centeva.SharedKernel.Interfaces;
using Centeva.SharedKernel.UnitTests.Fixtures;
using MediatR;

namespace Centeva.SharedKernel.UnitTests;

public class DomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly DomainEventDispatcher _sut;

    public DomainEventDispatcherTests()
    {
        _sut = new DomainEventDispatcher(_publisher);
    }

    [Fact]
    public async Task DispatchAndClearEvents_DispatchesEvents()
    {
        var entity = new TestEntity("test");
        entity.ChangeName("new name");
        var eventToPublish = entity.DomainEvents.First();

        await _sut.DispatchAndClearEvents(new List<BaseEntity> { entity });

        Mock.Get(_publisher).Verify(x => x.Publish(It.Is<BaseDomainEvent>(x => x == eventToPublish), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DispatchAndClearEvents_ClearsEvents()
    {
        var entity = new TestEntity("test");
        entity.ChangeName("new name");

        await _sut.DispatchAndClearEvents(new List<BaseEntity> { entity });

        entity.DomainEvents.Should().BeEmpty();
    }
}

using System.Reflection.Emit;
using Centeva.SharedKernel.Interfaces;
using Centeva.SharedKernel.UnitTests.Fixtures;
using MediatR;

namespace Centeva.SharedKernel.UnitTests;

public class DomainEventDispatcherTests
{
    private readonly IPublisher _publisher = Mock.Of<IPublisher>();
    private readonly DomainEventDispatcher _sut;
    private readonly TestGuidEntity _anotherEntity;
    private readonly TestEntity _entity;

    public DomainEventDispatcherTests()
    {
        _sut = new DomainEventDispatcher(_publisher);

        _entity = new TestEntity("test");
        _entity.ChangeName("new name");
        _anotherEntity = new TestGuidEntity("test");
        _anotherEntity.ChangeLabel("new label");
    }

    [Fact]
    public async Task DispatchAndClearEvents_DispatchesEvents()
    {
        await _sut.DispatchAndClearEvents(new List<IEntityWithEvents> { _entity, _anotherEntity });

        Mock.Get(_publisher).Verify(x => x.Publish(It.Is<BaseDomainEvent>(ev => ev is NameChanged), It.IsAny<CancellationToken>()), Times.Once);
        Mock.Get(_publisher).Verify(x => x.Publish(It.Is<BaseDomainEvent>(ev => ev is LabelChanged), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DispatchAndClearEvents_ClearsEvents()
    {
        await _sut.DispatchAndClearEvents(new List<IEntityWithEvents> { _entity, _anotherEntity });

        _entity.DomainEvents.Should().BeEmpty();
        _anotherEntity.DomainEvents.Should().BeEmpty();
    }
}

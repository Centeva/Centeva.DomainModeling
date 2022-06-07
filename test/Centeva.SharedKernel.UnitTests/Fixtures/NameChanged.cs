namespace Centeva.SharedKernel.UnitTests.Fixtures;

public class NameChanged : BaseDomainEvent
{
    public TestEntity Entity { get; init; }

    public NameChanged(TestEntity testEntity)
    {
        Entity = testEntity;
    }
}
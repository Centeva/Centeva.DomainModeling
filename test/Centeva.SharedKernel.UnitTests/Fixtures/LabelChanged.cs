namespace Centeva.SharedKernel.UnitTests.Fixtures;

public class LabelChanged : BaseDomainEvent
{
    public TestGuidEntity Entity { get; init; }

    public LabelChanged(TestGuidEntity testEntity)
    {
        Entity = testEntity;
    }
}
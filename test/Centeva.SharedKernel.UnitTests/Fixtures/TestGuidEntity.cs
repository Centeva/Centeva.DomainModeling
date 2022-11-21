namespace Centeva.SharedKernel.UnitTests.Fixtures;

public class TestGuidEntity : BaseEntity<Guid>
{
    public string Label { get; set; }

    public TestGuidEntity(string label)
    {
        Label = label;
    }

    public void ChangeLabel(string newName)
    {
        Label = newName;
        RegisterDomainEvent(new LabelChanged(this));
    }
}

namespace Centeva.SharedKernel.UnitTests.Fixtures;

public class TestEntity : BaseEntity
{
    public string Name { get; set; }

    public TestEntity(string name)
    {
        Name = name;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
        RegisterDomainEvent(new NameChanged(this));
    }
}
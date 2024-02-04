namespace Centeva.DomainModeling.UnitTests.Fixtures.Entities;

public class Person : BaseEntity<Guid>
{
    public string Name { get; set; }
    public List<Address> Addresses { get; init; } = new();

    public Person(Guid id, string name)
    {
        Id = id;
        Name = name;
        RegisterDomainEvent(new PersonCreatedEvent(this));
    }
}
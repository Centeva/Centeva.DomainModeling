namespace Centeva.SharedKernel.UnitTests.Fixtures.Entities;

public class Person : BaseEntity<Guid>
{
    public string Name { get; init; }
    public List<Address> Addresses { get; init; } = new();

    public Person(Guid id, string name)
    {
        Id = id;
        Name = name;
        RegisterDomainEvent(new PersonCreatedEvent(this));
    }
}
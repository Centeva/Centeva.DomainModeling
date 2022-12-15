namespace Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;

public class Person : BaseEntity
{
    public string? Name { get; set; }
    public List<Address> Addresses { get; set; } = new List<Address>();

    public Person(string name)
    {
        Name = name;
        RegisterDomainEvent(new PersonCreatedEvent());
    }

    public Person() { }
}
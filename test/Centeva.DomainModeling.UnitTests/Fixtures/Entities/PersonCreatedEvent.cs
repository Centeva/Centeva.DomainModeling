namespace Centeva.DomainModeling.UnitTests.Fixtures.Entities;

public class PersonCreatedEvent : BaseDomainEvent
{
    public Person Person { get; }

    public PersonCreatedEvent(Person person)
    {
        Person = person;
    }
}

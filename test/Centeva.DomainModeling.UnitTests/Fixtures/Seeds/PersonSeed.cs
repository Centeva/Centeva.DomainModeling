using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Seeds;

public static class PersonSeed
{
    public static readonly Guid ValidPersonId = Guid.NewGuid();
    public static readonly string ValidPersonName = "John Doe";
    public static readonly Guid ValidPersonId2 = Guid.NewGuid();
    public static readonly Guid ValidPersonId3 = Guid.NewGuid();

    public static List<Person> Get() =>
        new List<Person>
        {
            new Person(ValidPersonId, ValidPersonName),
            new Person(ValidPersonId2, "Jane Doe"),
            new Person(ValidPersonId3, "Joe Test")
        };
}

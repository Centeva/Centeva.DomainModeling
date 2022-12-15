using Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.IntegrationTests.Fixtures.Seeds;

public static class PersonSeed
{
    public const int ValidPersonId = 1;
    public const string ValidPersonName = "John Doe";

    public static List<Person> Get() =>
        new List<Person>
        {
            new Person()
            {
                Id = ValidPersonId,
                Name = ValidPersonName,
            },
            new Person()
            {
                Id = 2,
                Name = "Jane Doe",
            },
            new Person()
            {
                Id = 3,
                Name = "Joe Test"
            }
        };
}

using Centeva.SharedKernel.UnitTests.Fixtures.Entities;

namespace Centeva.SharedKernel.UnitTests.Fixtures.Seeds;

public static class AddressSeed
{
    public static List<Address> Get() =>
        new List<Address>
        {
            new Address()
            {
                Id = 1,
                Street = "123 Main",
                PostalCode = "00000",
                City = "Anytown",
                PersonId = PersonSeed.ValidPersonId
            },
            new Address()
            {
                Id = 2,
                Street = "PO Box 1",
                PostalCode = "00000",
                City = "Anytown",
                PersonId = PersonSeed.ValidPersonId
            },
            new Address()
            {
                Id = 3,
                Street = "1 South Highway",
                PostalCode = "12345",
                City = "Somewhere",
                PersonId = PersonSeed.ValidPersonId2
            },
            new Address()
            {
                Id = 4,
                Street = "1 Out There Road",
                PostalCode = "99999",
                City = "Erewhon",
                PersonId = PersonSeed.ValidPersonId2
            },
                new Address()
            {
                Id = 5,
                Street = "500 South Main, Apt 1",
                PostalCode = "11111",
                City = "Beach Town",
                PersonId = PersonSeed.ValidPersonId3
            }
        };
}

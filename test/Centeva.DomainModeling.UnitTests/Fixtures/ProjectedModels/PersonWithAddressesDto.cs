using System.Linq.Expressions;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;

public class PersonWithAddressesDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<AddressDto> Addresses { get; set; } = new();

    public static Expression<Func<Person, PersonWithAddressesDto>> FromPerson = 
        person => new PersonWithAddressesDto
        {
            Id = person.Id, 
            Name = person.Name,
            Addresses = person.Addresses.Select(a => new AddressDto
            {
                City = a.City,
                Street = a.Street,
                PostalCode = a.PostalCode
            }).ToList()
        };
}

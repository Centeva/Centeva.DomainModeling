using System.Linq.Expressions;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;

public class PersonDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public static Expression<Func<Person, PersonDto>> FromPerson = 
        person => new PersonDto
        {
            Id = person.Id, 
            Name = person.Name
        };
}

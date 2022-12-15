using System.Linq.Expressions;
using Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.IntegrationTests.Fixtures.ProjectedModels;

public class PersonDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public static Expression<Func<Person, PersonDto>> FromPerson = 
        person => new PersonDto
        {
            Id = person.Id, 
            Name = person.Name
        };
}

using System.Linq.Expressions;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;

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

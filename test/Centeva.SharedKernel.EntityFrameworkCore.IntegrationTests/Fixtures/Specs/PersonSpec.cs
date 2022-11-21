using Ardalis.Specification;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Specs;
public class PersonSpec : Specification<Person>
{
    public PersonSpec()
    {
        Query.OrderBy(x => x.Name);
    }
}

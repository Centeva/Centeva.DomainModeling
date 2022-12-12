using Ardalis.Specification;
using Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.IntegrationTests.Fixtures.Specs;
public class PersonSpec : Specification<Person>
{
    public PersonSpec()
    {
        Query.OrderBy(x => x.Name);
    }
}

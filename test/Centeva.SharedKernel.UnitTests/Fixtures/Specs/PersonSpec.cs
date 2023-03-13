using Ardalis.Specification;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;

namespace Centeva.SharedKernel.UnitTests.Fixtures.Specs;
public class PersonSpec : Specification<Person>
{
    public PersonSpec()
    {
        Query.OrderBy(x => x.Name);
    }
}

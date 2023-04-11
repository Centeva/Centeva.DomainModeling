using Ardalis.Specification;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Specs;
public class PersonSpec : Specification<Person>
{
    public PersonSpec()
    {
        Query.OrderBy(x => x.Name);
    }
}

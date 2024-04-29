using Ardalis.Specification;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Specs;
public class PersonByNameCaseInsensitiveSpec : Specification<Person>, ISingleResultSpecification<Person>
{
    public PersonByNameCaseInsensitiveSpec(string name)
    {
        Query
            .Where(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}

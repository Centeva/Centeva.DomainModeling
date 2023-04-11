using Ardalis.Specification;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Specs;
public class PersonByNameSpec : Specification<Person>, ISingleResultSpecification<Person>
{
    public PersonByNameSpec(string name)
    {
        Query
            .Search(x => x.Name!, "%" + name + "%");
    }
}

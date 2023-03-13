using Ardalis.Specification;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;

namespace Centeva.SharedKernel.UnitTests.Fixtures.Specs;
public class PersonByNameSpec : Specification<Person>, ISingleResultSpecification<Person>
{
    public PersonByNameSpec(string name)
    {
        Query
            .Search(x => x.Name!, "%" + name + "%");
    }
}

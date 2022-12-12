using Ardalis.Specification;
using Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.IntegrationTests.Fixtures.Specs;
public class PersonByNameSpec : Specification<Person>, ISingleResultSpecification<Person>
{
    public PersonByNameSpec(string name)
    {
        Query
            .Search(x => x.Name!, "%" + name + "%");
    }
}

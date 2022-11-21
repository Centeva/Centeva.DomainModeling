using Ardalis.Specification;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Specs;
public class PersonByNameSpec : Specification<Person>, ISingleResultSpecification<Person>
{
    public PersonByNameSpec(string name)
    {
        Query
            .Search(x => x.Name!, "%" + name + "%");
    }
}

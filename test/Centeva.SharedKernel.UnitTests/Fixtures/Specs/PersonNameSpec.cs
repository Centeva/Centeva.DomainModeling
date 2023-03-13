using Ardalis.Specification;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;

namespace Centeva.SharedKernel.UnitTests.Fixtures.Specs;
public class PersonNameSpec : Specification<Person, string>, ISingleResultSpecification<Person, string>
{
    public PersonNameSpec(Guid id)
    {
        Query
            .Select(x => x.Name)
            .Where(x => x.Id == id);
    }
}

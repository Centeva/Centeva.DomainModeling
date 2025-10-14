using Ardalis.Specification;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Specs;
public class PersonNameSpec : Specification<Person, string>, ISingleResultSpecification<Person, string>
{
    public PersonNameSpec(Guid id)
    {
        Query
            .Where(x => x.Id == id)
            .Select(x => x.Name);
    }
}

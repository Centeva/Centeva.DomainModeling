using Ardalis.Specification;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Specs;
public class PersonWithPostProcessingSpec : Specification<Person>
{
    public PersonWithPostProcessingSpec()
    {
        Query
            .OrderBy(x => x.Name)
            .PostProcessingAction(people => people.Reverse());
    }
}

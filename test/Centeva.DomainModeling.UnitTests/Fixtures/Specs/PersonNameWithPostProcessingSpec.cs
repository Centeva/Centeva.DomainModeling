using Ardalis.Specification;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.Fixtures.Specs;
public class PersonNameWithPostProcessingSpec : Specification<Person, string>
{
    public PersonNameWithPostProcessingSpec()
    {
        Query
            .OrderBy(x => x.Name)
            .Select(x => x.Name);

        Query.PostProcessingAction(names => names.Reverse());
    }
}

using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class FirstOrDefaultAsyncTests : IntegrationTestBase
{
    public FirstOrDefaultAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.FirstOrDefaultAsync<PersonDto>(new PersonSpec(), PersonDto.FromPerson, CancellationToken.None);

        result.ShouldNotBeNull();
        result!.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.FirstOrDefaultAsync<PersonWithAddressesDto>(new PersonSpec(), PersonWithAddressesDto.FromPerson);

        result.ShouldNotBeNull();
        result!.Addresses.ShouldNotBeEmpty();
        result.Addresses[0].Street.ShouldNotBeNullOrWhiteSpace();
    }
}

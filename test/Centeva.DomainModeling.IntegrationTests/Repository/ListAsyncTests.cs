using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class ListAsyncTests : IntegrationTestBase
{
    public ListAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.ListAsync<PersonDto>(PersonDto.FromPerson);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result[0].Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.ListAsync<PersonWithAddressesDto>(PersonWithAddressesDto.FromPerson);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result[0].Addresses.ShouldNotBeEmpty();
        result[0].Addresses[0].Street.ShouldNotBeNullOrWhiteSpace();
    }
}

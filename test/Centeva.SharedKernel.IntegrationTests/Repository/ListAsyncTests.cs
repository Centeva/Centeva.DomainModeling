using Centeva.SharedKernel.IntegrationTests.Fixtures;
using Centeva.SharedKernel.UnitTests.Fixtures.ProjectedModels;
using FluentAssertions;

namespace Centeva.SharedKernel.IntegrationTests.Repository;

public class ListAsyncTests : IntegrationTestBase
{
    public ListAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.ListAsync<PersonDto>(PersonDto.FromPerson);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.ListAsync<PersonWithAddressesDto>(PersonWithAddressesDto.FromPerson);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Addresses.Should().NotBeEmpty();
        result[0].Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

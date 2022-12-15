using Centeva.SharedKernel.IntegrationTests.Fixtures;
using Centeva.SharedKernel.IntegrationTests.Fixtures.ProjectedModels;
using FluentAssertions;

namespace Centeva.SharedKernel.IntegrationTests.ProjectedRepository;

public class ListProjectedAsyncTests : IntegrationTestBase
{
    public ListProjectedAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task ProjectsToSimpleDto()
    {
        var result = await _personRepository.ListProjectedAsync<PersonDto>();

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProjectsToNestedDto()
    {
        var result = await _personRepository.ListProjectedAsync<PersonWithAddressesDto>();

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Addresses.Should().NotBeEmpty();
        result[0].Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

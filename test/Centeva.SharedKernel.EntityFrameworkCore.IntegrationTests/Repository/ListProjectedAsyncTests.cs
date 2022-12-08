using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;
using FluentAssertions;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Repository;

public class ListProjectedAsyncTests : IntegrationTestBase
{
    public ListProjectedAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithAutoMapper_ProjectsToSimpleDto()
    {
        var result = await _personRepository.ListProjectedAsync<PersonDto>();

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithAutoMapper_ProjectsToNestedDto()
    {
        var result = await _personRepository.ListProjectedAsync<PersonWithAddressesDto>();

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Addresses.Should().NotBeEmpty();
        result[0].Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.ListProjectedAsync<PersonDto>(PersonDto.FromPerson);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.ListProjectedAsync<PersonWithAddressesDto>(PersonWithAddressesDto.FromPerson);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Addresses.Should().NotBeEmpty();
        result[0].Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

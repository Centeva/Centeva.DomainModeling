using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Specs;
using FluentAssertions;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Repository;

public class FirstOrDefaultProjectedAsyncTests : IntegrationTestBase
{
    public FirstOrDefaultProjectedAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task ProjectsToSimpleDto()
    {
        var result = await _personRepository.FirstOrDefaultProjectedAsync<PersonDto>(new PersonSpec());

        result.Should().NotBeNull();
        result!.Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProjectsToNestedDto()
    {
        var result = await _personRepository.FirstOrDefaultProjectedAsync<PersonWithAddressesDto>(new PersonSpec());

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

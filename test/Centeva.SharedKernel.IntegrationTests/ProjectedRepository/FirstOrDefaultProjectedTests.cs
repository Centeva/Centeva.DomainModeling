using Centeva.SharedKernel.IntegrationTests.Fixtures;
using Centeva.SharedKernel.IntegrationTests.Fixtures.ProjectedModels;
using Centeva.SharedKernel.IntegrationTests.Fixtures.Specs;
using FluentAssertions;

namespace Centeva.SharedKernel.IntegrationTests.ProjectedRepository;

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

using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.IntegrationTests.ProjectedRepository;

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

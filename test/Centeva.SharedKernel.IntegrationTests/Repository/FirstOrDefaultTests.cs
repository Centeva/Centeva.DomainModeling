using Centeva.SharedKernel.IntegrationTests.Fixtures;
using Centeva.SharedKernel.IntegrationTests.Fixtures.ProjectedModels;
using Centeva.SharedKernel.IntegrationTests.Fixtures.Specs;
using FluentAssertions;

namespace Centeva.SharedKernel.IntegrationTests.Repository;

public class FirstOrDefaultAsyncTests : IntegrationTestBase
{
    public FirstOrDefaultAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.FirstOrDefaultAsync<PersonDto>(new PersonSpec(), PersonDto.FromPerson, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.FirstOrDefaultAsync<PersonWithAddressesDto>(new PersonSpec(), PersonWithAddressesDto.FromPerson);

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

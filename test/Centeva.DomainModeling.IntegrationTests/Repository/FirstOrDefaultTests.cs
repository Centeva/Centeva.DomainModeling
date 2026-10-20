using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class FirstOrDefaultAsyncTests : IntegrationTestBase
{
    public FirstOrDefaultAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithSpecification_ReturnsFirstMatch()
    {
        var result = await _personRepository.FirstOrDefaultAsync(new PersonSpec(), TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Jane Doe");
    }

    [Fact]
    public async Task WithSpecification_ReturnsNullWhenNoMatches()
    {
        var result = await _personRepository.FirstOrDefaultAsync(new PersonByNameSpec("Not Found"), TestContext.Current.CancellationToken);

        result.Should().BeNull();
    }

    [Fact]
    public async Task WithResultSpecification_ReturnsProjectedResult()
    {
        var result = await _personRepository.FirstOrDefaultAsync(new PersonNameSpec(PersonSeed.ValidPersonId), TestContext.Current.CancellationToken);

        result.Should().Be(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.FirstOrDefaultAsync<PersonDto>(new PersonSpec(), PersonDto.FromPerson, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.FirstOrDefaultAsync<PersonWithAddressesDto>(new PersonSpec(), PersonWithAddressesDto.FromPerson, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

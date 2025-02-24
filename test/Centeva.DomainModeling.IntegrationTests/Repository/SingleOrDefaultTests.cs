using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class SingleOrDefaultAsyncTests : IntegrationTestBase
{
    public SingleOrDefaultAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.SingleOrDefaultAsync<PersonDto>(new PersonByNameSpec(PersonSeed.ValidPersonName), PersonDto.FromPerson);

        result.ShouldNotBeNull();
        result!.Name.ShouldBe(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.SingleOrDefaultAsync<PersonWithAddressesDto>(new PersonByNameSpec(PersonSeed.ValidPersonName), PersonWithAddressesDto.FromPerson);

        result.ShouldNotBeNull();
        result!.Addresses.ShouldNotBeEmpty();
        result.Addresses[0].Street.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ThrowsExceptionWhenMultiple()
    {
        var act = () => _personRepository.SingleOrDefaultAsync<PersonDto>(new PersonByNameSpec("Doe"), PersonDto.FromPerson);

        await act.ShouldThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task WithExpression_ReturnsNullWhenNoMatches()
    {
        var result = await _personRepository.SingleOrDefaultAsync<PersonDto>(new PersonByNameSpec("Not Found"), PersonDto.FromPerson);

        result.ShouldBeNull();
    }
}

using Centeva.SharedKernel.IntegrationTests.Fixtures;
using Centeva.SharedKernel.UnitTests.Fixtures.ProjectedModels;
using Centeva.SharedKernel.UnitTests.Fixtures.Seeds;
using Centeva.SharedKernel.UnitTests.Fixtures.Specs;
using FluentAssertions;

namespace Centeva.SharedKernel.IntegrationTests.Repository;

public class SingleOrDefaultAsyncTests : IntegrationTestBase
{
    public SingleOrDefaultAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.SingleOrDefaultAsync<PersonDto>(new PersonByNameSpec(PersonSeed.ValidPersonName), PersonDto.FromPerson);

        result.Should().NotBeNull();
        result!.Name.Should().Be(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.SingleOrDefaultAsync<PersonWithAddressesDto>(new PersonByNameSpec(PersonSeed.ValidPersonName), PersonWithAddressesDto.FromPerson);

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ThrowsExceptionWhenMultiple()
    {
        var act = () => _personRepository.SingleOrDefaultAsync<PersonDto>(new PersonByNameSpec("Doe"), PersonDto.FromPerson);

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task WithExpression_ReturnsNullWhenNoMatches()
    {
        var result = await _personRepository.SingleOrDefaultAsync<PersonDto>(new PersonByNameSpec("Not Found"), PersonDto.FromPerson);

        result.Should().BeNull();
    }
}

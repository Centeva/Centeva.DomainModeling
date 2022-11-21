using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Seeds;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Specs;
using FluentAssertions;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Repository;

public class SingleOrDefaultProjectedAsyncTests : IntegrationTestBase
{
    public SingleOrDefaultProjectedAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task ProjectsToSimpleDto()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().NotBeNull();
        result!.Name.Should().Be(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task ProjectsToNestedDto()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonWithAddressesDto>(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ThrowsExceptionWhenMultiple()
    {
        var act = () => _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec("Doe"));

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ReturnsNullWhenNoMatches()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec("Not Found"));

        result.Should().BeNull();
    }
}

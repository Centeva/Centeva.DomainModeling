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
    public async Task WithAutoMapper_ProjectsToSimpleDto()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().NotBeNull();
        result!.Name.Should().Be(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task WithAutoMapper_ProjectsToNestedDto()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonWithAddressesDto>(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithAutoMapper_ThrowsExceptionWhenMultiple()
    {
        var act = () => _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec("Doe"));

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task WithAutoMapper_ReturnsNullWhenNoMatches()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec("Not Found"));

        result.Should().BeNull();
    }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec(PersonSeed.ValidPersonName), PersonDto.FromPerson);

        result.Should().NotBeNull();
        result!.Name.Should().Be(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonWithAddressesDto>(new PersonByNameSpec(PersonSeed.ValidPersonName), PersonWithAddressesDto.FromPerson);

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ThrowsExceptionWhenMultiple()
    {
        var act = () => _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec("Doe"), PersonDto.FromPerson);

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task WithExpression_ReturnsNullWhenNoMatches()
    {
        var result = await _personRepository.SingleOrDefaultProjectedAsync<PersonDto>(new PersonByNameSpec("Not Found"), PersonDto.FromPerson);

        result.Should().BeNull();
    }
}

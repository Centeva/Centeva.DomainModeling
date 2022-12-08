using System.Linq.Expressions;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Specs;
using FluentAssertions;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Repository;

public class FirstOrDefaultProjectedAsyncTests : IntegrationTestBase
{
    public FirstOrDefaultProjectedAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task WithAutoMapper_ProjectsToSimpleDto()
    {
        var result = await _personRepository.FirstOrDefaultProjectedAsync<PersonDto>(new PersonSpec());

        result.Should().NotBeNull();
        result!.Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithAutoMapper_ProjectsToNestedDto()
    {
        var result = await _personRepository.FirstOrDefaultProjectedAsync<PersonWithAddressesDto>(new PersonSpec());

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.FirstOrDefaultProjectedAsync<PersonDto>(new PersonSpec(), PersonDto.FromPerson, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.FirstOrDefaultProjectedAsync<PersonWithAddressesDto>(new PersonSpec(), PersonWithAddressesDto.FromPerson);

        result.Should().NotBeNull();
        result!.Addresses.Should().NotBeEmpty();
        result.Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

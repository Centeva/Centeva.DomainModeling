using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class ListAsyncTests : IntegrationTestBase
{
    public ListAsyncTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task ReturnsAllEntities()
    {
        var result = await _personRepository.ListAsync(TestContext.Current.CancellationToken);

        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task WithSpecification_ReturnsMatchingEntitiesInOrder()
    {
        var result = await _personRepository.ListAsync(new PersonSpec(), TestContext.Current.CancellationToken);

        result.Select(x => x.Name).Should().Equal("Jane Doe", "Joe Test", "John Doe");
    }

    [Fact]
    public async Task WithSpecification_AppliesPostProcessingAction()
    {
        var result = await _personRepository.ListAsync(new PersonWithPostProcessingSpec(), TestContext.Current.CancellationToken);

        result.Select(x => x.Name).Should().Equal("John Doe", "Joe Test", "Jane Doe");
    }

    [Fact]
    public async Task WithResultSpecification_ReturnsProjectedResults()
    {
        var result = await _personRepository.ListAsync(new PersonNameSpec(PersonSeed.ValidPersonId), TestContext.Current.CancellationToken);

        result.Should().Equal(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task WithResultSpecification_AppliesPostProcessingAction()
    {
        var result = await _personRepository.ListAsync(new PersonNameWithPostProcessingSpec(), TestContext.Current.CancellationToken);

        result.Should().Equal("John Doe", "Joe Test", "Jane Doe");
    }

    [Fact]
    public async Task WithSpecificationAndExpression_ProjectsMatchingEntities()
    {
        var result = await _personRepository.ListAsync<PersonDto>(new PersonByNameSpec("Doe"), PersonDto.FromPerson, TestContext.Current.CancellationToken);

        result.Select(x => x.Name).Should().BeEquivalentTo("John Doe", "Jane Doe");
    }

    [Fact]
    public async Task WithExpression_ProjectsToSimpleDto()
    {
        var result = await _personRepository.ListAsync<PersonDto>(PersonDto.FromPerson, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Name.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task WithExpression_ProjectsToNestedDto()
    {
        var result = await _personRepository.ListAsync<PersonWithAddressesDto>(PersonWithAddressesDto.FromPerson, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result[0].Addresses.Should().NotBeEmpty();
        result[0].Addresses[0].Street.Should().NotBeNullOrWhiteSpace();
    }
}

using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;

namespace Centeva.DomainModeling.IntegrationTests.Repository;

public class GetByIdTests : IntegrationTestBase
{
    public GetByIdTests(SharedDatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public async Task GetByIdAsync_ReturnsMatchingEntity()
    {
        var result = await _personRepository.GetByIdAsync(PersonSeed.ValidPersonId, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Name.Should().Be(PersonSeed.ValidPersonName);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNullWhenNotFound()
    {
        var result = await _personRepository.GetByIdAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        result.Should().BeNull();
    }
}

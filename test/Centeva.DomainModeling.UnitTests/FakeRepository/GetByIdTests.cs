using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;
public class GetByIdTests
{
    private readonly FakeRepository<Person, Guid>  _repository = new();

    [Fact]
    public async Task ReturnsMatchingEntity()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.GetByIdAsync(PersonSeed.ValidPersonId, TestContext.Current.CancellationToken);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task ReturnsNullWhenNotFound()
    {
        var result = await _repository.GetByIdAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        result.ShouldBeNull();
    }
}

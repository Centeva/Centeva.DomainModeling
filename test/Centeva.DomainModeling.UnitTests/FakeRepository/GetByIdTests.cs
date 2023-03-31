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
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.GetByIdAsync(PersonSeed.ValidPersonId);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnsNullWhenNotFound()
    {
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        result.Should().BeNull();
    }
}

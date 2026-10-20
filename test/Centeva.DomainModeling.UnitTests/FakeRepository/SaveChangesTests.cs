using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;

public class SaveChangesTests
{
    private readonly FakeRepository<Person, Guid> _repository = new();

    [Fact]
    public async Task SaveChangesAsync_ReturnsZero()
    {
        await _repository.AddAsync(new Person(Guid.NewGuid(), "Test"), TestContext.Current.CancellationToken);

        var result = await _repository.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Changes are applied immediately, so there is never anything pending to save
        result.Should().Be(0);
    }
}

using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;

public class DeleteTests
{
    private readonly List<Person> _entities;
    private readonly FakeRepository<Person, Guid> _repository;

    public DeleteTests()
    {
        _entities = PersonSeed.Get();
        _repository = new FakeRepository<Person, Guid>(_entities);
    }

    [Fact]
    public async Task DeleteAsync_RemovesFromRepository()
    {
        var entityToDelete = _entities[0];
        await _repository.DeleteAsync(entityToDelete);

        _entities.Should().NotContain(entityToDelete);
    }

    [Fact]
    public async Task DeleteAsync_WhenNotFound_DoesNothing()
    {
        var entityToDelete = new Person(Guid.NewGuid(), "Test");
        await _repository.DeleteAsync(entityToDelete);

        _entities.Should().NotContain(entityToDelete);
        _entities.Should().HaveCount(3);
    }

    [Fact]
    public async Task DeleteRangeAsync_DeletesAllFromRepository()
    {
        await _repository.DeleteRangeAsync(_entities.ToList());

        _entities.Should().BeEmpty();
    }
}

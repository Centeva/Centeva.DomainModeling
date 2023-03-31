using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;

public class UpdateTests
{
    private readonly List<Person> _entities = new();
    private readonly FakeRepository<Person, Guid> _repository;

    public UpdateTests()
    {
        _entities.Add(new Person(Guid.NewGuid(), "Test"));
        _repository = new FakeRepository<Person, Guid>(_entities);
    }

    [Fact]
    public async Task UpdateAsync_ModifiesMatchingEntity()
    {
        var update = new Person(_entities[0].Id, "New name");
        await _repository.UpdateAsync(update);

        _entities.Should().ContainSingle(x => x.Id == update.Id && x.Name == update.Name);
    }

    [Fact]
    public async Task UpdateRangeAsync_ModifiesMatchingEntities()
    {
        _entities.Add(new Person(Guid.NewGuid(), "Test 2"));

        var update = new List<Person>
        {
            new Person(_entities[0].Id, "New name"), new Person(_entities[1].Id, "New name 2")
        };

        await _repository.UpdateRangeAsync(update);

        _entities.Should().Satisfy(
            first => first.Id == update[0].Id && first.Name == update[0].Name,
            second => second.Id == update[1].Id && second.Name == update[1].Name);
    }
}

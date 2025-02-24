using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;

public class AddTests
{
    private readonly List<Person> _entities = new();
    private readonly FakeRepository<Person, Guid> _repository;

    public AddTests()
    {
        _repository = new FakeRepository<Person, Guid>(_entities);
    }

    [Fact]
    public async Task AddAsync_AddsToRepository()
    {
        var person = new Person(Guid.NewGuid(), "Test");
        await _repository.AddAsync(person);

        _entities.ShouldContain(x => x == person);
    }

    [Fact]
    public async Task AddAsync_ReturnsAddedEntity()
    {
        var person = new Person(Guid.NewGuid(), "Test");
        var result = await _repository.AddAsync(person);

        result.ShouldBe(person);
    }

    [Fact]
    public async Task AddRangeAsync_AddsAllToRepository()
    {
        var people = PersonSeed.Get();
        await _repository.AddRangeAsync(people);

        _entities.ShouldBeEquivalentTo(people);
    }

    [Fact]
    public async Task AddRangeAsync_ReturnsAddedEntities()
    {
        var people = PersonSeed.Get();
        var result = await _repository.AddRangeAsync(people);

        result.ShouldBeEquivalentTo(people);
    }
}

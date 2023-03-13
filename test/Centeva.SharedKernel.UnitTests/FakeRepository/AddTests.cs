using Centeva.SharedKernel.Testing;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;
using Centeva.SharedKernel.UnitTests.Fixtures.Seeds;

namespace Centeva.SharedKernel.UnitTests.FakeRepository;

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

        _entities.Should().ContainSingle(x => x == person);
    }

    [Fact]
    public async Task AddAsync_ReturnsAddedEntity()
    {
        var person = new Person(Guid.NewGuid(), "Test");
        var result = await _repository.AddAsync(person);

        result.Should().Be(person);
    }

    [Fact]
    public async Task AddRangeAsync_AddsAllToRepository()
    {
        var people = PersonSeed.Get();
        await _repository.AddRangeAsync(people);

        _entities.Should().BeEquivalentTo(people);
    }

    [Fact]
    public async Task AddRangeAsync_ReturnsAddedEntities()
    {
        var people = PersonSeed.Get();
        var result = await _repository.AddRangeAsync(people);

        result.Should().BeEquivalentTo(people);
    }
}

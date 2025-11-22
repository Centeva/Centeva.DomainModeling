using Ardalis.Specification;
using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;
public class SingleOrDefaultTests
{
    private readonly FakeRepository<Person, Guid> _repository = new();

    [Fact]
    public async Task WithEntryMatchingSpec_ReturnsEntry()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.SingleOrDefaultAsync(new PersonByNameSpec(PersonSeed.ValidPersonName), TestContext.Current.CancellationToken);

        result.ShouldNotBeNull();
        result!.Id.ShouldBe(PersonSeed.ValidPersonId);
    }

    [Fact]
    public async Task WithoutMatch_ReturnsNull()
    {
        var result = await _repository.SingleOrDefaultAsync(new PersonByNameSpec("bad"), TestContext.Current.CancellationToken);

        result.ShouldBeNull();
    }

    [Fact]
    public async Task WithMultipleMatches_ThrowsException()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var spec = new SingleResultSpecification<Person>(); // no filter

        var act = () => _repository.SingleOrDefaultAsync(spec);

        await act.ShouldThrowAsync<Exception>();
    }

    [Fact]
    public async Task WithSelectSpec_ReturnsEntry()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.SingleOrDefaultAsync(new PersonNameSpec(PersonSeed.ValidPersonId), TestContext.Current.CancellationToken);

        result.ShouldBe(PersonSeed.ValidPersonName);
    }
}

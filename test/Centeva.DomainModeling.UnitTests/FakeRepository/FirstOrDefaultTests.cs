using Centeva.DomainModeling.Testing;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Centeva.DomainModeling.UnitTests.Fixtures.Specs;

namespace Centeva.DomainModeling.UnitTests.FakeRepository;
public class FirstOrDefaultTests
{
    private readonly FakeRepository<Person, Guid> _repository = new();

    [Fact]
    public async Task WithEntryMatchingSpec_ReturnsFirst()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.FirstOrDefaultAsync(new PersonByNameSpec(PersonSeed.ValidPersonName), TestContext.Current.CancellationToken);

        result.ShouldNotBeNull();
        result!.Id.ShouldBe(PersonSeed.ValidPersonId);
    }

    [Fact]
    public async Task WithoutMatch_ReturnsNull()
    {
        var result = await _repository.FirstOrDefaultAsync(new PersonByNameSpec("bad"), TestContext.Current.CancellationToken);

        result.ShouldBeNull();
    }

    [Fact]
    public async Task WithExpression_ProjectsToAnotherType()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.FirstOrDefaultAsync(new PersonByNameSpec(PersonSeed.ValidPersonName), x => x.Id, CancellationToken.None);

        result.ShouldBe(PersonSeed.ValidPersonId);
    }

    [Fact]
    public async Task WithSelectSpec_ReturnsFirst()
    {
        await _repository.AddRangeAsync(PersonSeed.Get(), TestContext.Current.CancellationToken);

        var result = await _repository.FirstOrDefaultAsync(new PersonNameSpec(PersonSeed.ValidPersonId), TestContext.Current.CancellationToken);

        result.ShouldBe(PersonSeed.ValidPersonName);
    }
}

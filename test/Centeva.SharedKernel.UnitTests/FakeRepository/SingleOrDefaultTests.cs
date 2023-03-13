using Ardalis.Specification;
using Centeva.SharedKernel.Testing;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;
using Centeva.SharedKernel.UnitTests.Fixtures.Seeds;
using Centeva.SharedKernel.UnitTests.Fixtures.Specs;

namespace Centeva.SharedKernel.UnitTests.FakeRepository;
public class SingleOrDefaultTests
{
    private readonly FakeRepository<Person, Guid> _repository = new();

    [Fact]
    public async Task WithEntryMatchingSpec_ReturnsEntry()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.SingleOrDefaultAsync(new PersonByNameSpec(PersonSeed.ValidPersonName));

        result.Should().NotBeNull();
        result!.Id.Should().Be(PersonSeed.ValidPersonId);
    }

    [Fact]
    public async Task WithoutMatch_ReturnsNull()
    {
        var result = await _repository.SingleOrDefaultAsync(new PersonByNameSpec("bad"));

        result.Should().BeNull();
    }

    [Fact]
    public async Task WithMultipleMatches_ThrowsException()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var spec = new SingleResultSpecification<Person>(); // no filter

        var act = () => _repository.SingleOrDefaultAsync(spec);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task WithSelectSpec_ReturnsEntry()
    {
        await _repository.AddRangeAsync(PersonSeed.Get());

        var result = await _repository.SingleOrDefaultAsync(new PersonNameSpec(PersonSeed.ValidPersonId));

        result.Should().Be(PersonSeed.ValidPersonName);
    }
}

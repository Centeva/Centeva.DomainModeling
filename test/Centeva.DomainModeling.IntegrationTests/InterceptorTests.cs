using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Moq;

namespace Centeva.DomainModeling.IntegrationTests;

public class InterceptorTests : IntegrationTestBase
{
    public InterceptorTests(SharedDatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task PostSaveEventDispatchingInterceptor_DispatchesEvents()
    {
        var person = new Person(Guid.NewGuid(), "Test");

        _dbContext.People.Add(person);
        await _dbContext.SaveChangesAsync();

        Mock.Get(_dispatcher)
            .Verify<Task>(
                x => x.DispatchAndClearEvents(It.IsAny<IEnumerable<ObjectWithEvents>>(), It.IsAny<CancellationToken>()),
                Times.Once);
    }
}
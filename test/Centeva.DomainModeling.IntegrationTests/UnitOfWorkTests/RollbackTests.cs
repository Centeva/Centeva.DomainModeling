using Centeva.DomainModeling.EFCore;
using Centeva.DomainModeling.IntegrationTests.Fixtures;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;

namespace Centeva.DomainModeling.IntegrationTests.UnitOfWorkTests;

public class RollbackTests : IntegrationTestBase
{
    private readonly UnitOfWork _unitOfWork;

    public RollbackTests(SharedDatabaseFixture fixture) : base(fixture)
    {
        _unitOfWork = new UnitOfWork(_dbContext);
    }

    [Fact]
    public async Task RollbackAfterAdd_DoesNotPersistChanges()
    {
        var person = new Person(Guid.NewGuid(), "Test");

        _unitOfWork.BeginTransaction();
        await _personRepository.AddAsync(person);
        _unitOfWork.Rollback();

        _dbContext.People.FirstOrDefault(x => x.Id == person.Id).Should().BeNull();
    }

    [Fact]
    public async Task RollbackAfterDelete_DoesNotPersist()
    {
        var person = new Person(Guid.NewGuid(), "Test");
        await _personRepository.AddAsync(person);
        
        _unitOfWork.BeginTransaction();
        await _personRepository.DeleteAsync(person);
        _unitOfWork.Rollback();

        _dbContext.People.FirstOrDefault(x => x.Id == person.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task RollbackAfterUpdate_DoesNotPersist()
    {
        var person = await _personRepository.GetByIdAsync(PersonSeed.ValidPersonId);
        var originalName = person!.Name;
        
        _unitOfWork.BeginTransaction();
        person.Name = "New Name";
        await _personRepository.UpdateAsync(person);
        _unitOfWork.Rollback();

        _dbContext.People.FirstOrDefault(x => x.Id == PersonSeed.ValidPersonId)!
            .Name.Should().Be(originalName);
    }
}
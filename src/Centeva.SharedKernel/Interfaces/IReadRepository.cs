using Ardalis.Specification;

namespace Centeva.SharedKernel.Interfaces;

public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class, IAggregateRoot
{
}


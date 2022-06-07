using Ardalis.Specification;

namespace Centeva.SharedKernel.Interfaces;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IAggregateRoot
{
}

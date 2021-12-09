namespace Centeva.SharedKernel.Interfaces;

/// <summary>
/// You can apply this interface to an entity to mark it as an aggregate
/// root.  Repositories should only work with aggregate roots and child
/// entities should be access and manipulated through the root.
/// </summary>
public interface IAggregateRoot
{
}
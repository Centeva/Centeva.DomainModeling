namespace Centeva.DomainModeling;

public interface IDomainEvent
{
    DateTime DateOccurred { get; }
}

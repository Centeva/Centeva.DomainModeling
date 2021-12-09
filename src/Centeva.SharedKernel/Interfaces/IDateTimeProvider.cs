namespace Centeva.SharedKernel.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}

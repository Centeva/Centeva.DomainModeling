namespace Centeva.DomainModeling;

// From https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/ValueObject.cs

/// <summary>
/// Base class for value objects that compare by their equality components rather than by reference.
/// </summary>
/// <remarks>
/// Before reaching for this base class, consider whether one of the built-in options fits better:
/// <list type="bullet">
///   <item>
///     For small values (roughly &lt;= 16 bytes) such as strongly-typed IDs, money amounts,
///     or coordinates, prefer a <c>readonly record struct</c>.  It gives you value equality
///     and immutability with no heap allocations.
///   </item>
///   <item>
///     For larger values, or values whose equality components include collections, prefer a
///     <c>record</c> (class).  Copying a large struct on every method call is wasteful, and
///     equality over collection references is cheap on a reference type.
///   </item>
/// </list>
/// Use <see cref="ValueObject"/> when you need something the built-in options cannot easily
/// express — most commonly, when you want to include only <em>some</em> of the object's
/// properties in equality (via <see cref="GetEqualityComponents"/>), or when the value object
/// participates in an inheritance hierarchy that structs cannot support.
/// </remarks>
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var component in GetEqualityComponents())
        {
            hash.Add(component);
        }
        return hash.ToHashCode();
    }

    public ValueObject? GetCopy()
    {
        return MemberwiseClone() as ValueObject;
    }
}

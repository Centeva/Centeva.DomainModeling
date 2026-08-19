namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Value object representing a customer's name.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ValueObject"/> rather than using a <c>readonly record struct</c>
/// because it holds multiple string fields, making it a reference-type value object.
/// This avoids copying multiple strings on every method call and allows equality to be
/// defined over selected components via <see cref="GetEqualityComponents"/>.
/// </remarks>
public class CustomerName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    public string FullName => $"{FirstName} {LastName}";

    public CustomerName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    public override string ToString() => FullName;
}

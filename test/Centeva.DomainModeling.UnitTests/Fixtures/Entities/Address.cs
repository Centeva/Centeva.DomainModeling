namespace Centeva.DomainModeling.UnitTests.Fixtures.Entities;

public class Address : BaseEntity<int>
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}
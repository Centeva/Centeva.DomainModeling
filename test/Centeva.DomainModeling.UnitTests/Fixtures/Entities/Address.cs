namespace Centeva.DomainModeling.UnitTests.Fixtures.Entities;

public class Address
{
    public int Id { get; set; }
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}
namespace Centeva.SharedKernel.IntegrationTests.Fixtures.Entities;

public class Address
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}
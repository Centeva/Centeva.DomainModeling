namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;

public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Address> Addresses { get; set; } = new List<Address>();
}
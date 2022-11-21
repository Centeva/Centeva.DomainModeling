namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;

public class PersonWithAddressesDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<AddressDto> Addresses { get; set; } = new();
}

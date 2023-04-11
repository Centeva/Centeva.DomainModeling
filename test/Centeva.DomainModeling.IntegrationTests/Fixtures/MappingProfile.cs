using AutoMapper;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.ProjectedModels;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Person, PersonWithAddressesDto>();
    }
}

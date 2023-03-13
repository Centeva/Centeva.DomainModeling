using AutoMapper;
using Centeva.SharedKernel.UnitTests.Fixtures.Entities;
using Centeva.SharedKernel.UnitTests.Fixtures.ProjectedModels;

namespace Centeva.SharedKernel.IntegrationTests.Fixtures;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Person, PersonWithAddressesDto>();
    }
}

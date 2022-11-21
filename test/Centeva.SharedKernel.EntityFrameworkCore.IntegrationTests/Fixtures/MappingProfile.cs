using AutoMapper;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.ProjectedModels;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Person, PersonWithAddressesDto>();
    }
}

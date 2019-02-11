using AutoMapper;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.RefuelAggregate;

namespace Travel.API.Infrastructure.MappingProfiles
{
    public class RefuelProfile : Profile
    {
        public RefuelProfile()
        {
            CreateMap<Refuel, RefuelItem>();
            CreateMap<RefuelItem, Refuel>();
        }
    }
}

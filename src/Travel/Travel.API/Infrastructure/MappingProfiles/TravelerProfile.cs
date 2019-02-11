using AutoMapper;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.API.Infrastructure.MappingProfiles
{
    public class TravelerProfile : Profile
    {
        public TravelerProfile()
        {
            CreateMap<Traveler, TravelerItem>();
            CreateMap<TravelerItem, Traveler>();
        }
    }
}

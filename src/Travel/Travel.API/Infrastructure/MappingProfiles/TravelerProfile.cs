using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.API.Model;
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

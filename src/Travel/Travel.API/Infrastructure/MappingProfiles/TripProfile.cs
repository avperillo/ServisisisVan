using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.TripAggregate;

namespace Travel.API.Infrastructure.MappingProfiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Trip, TripItem>();
            CreateMap<TripItem, Trip>();
        }
    }
}

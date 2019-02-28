using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Infrastructure;

namespace Travel.API.Infrastructure.Queries
{
    public class TripQueries : ITripQueries
    {
        private readonly TravelContext _context;
        private readonly IMapperService _mapper;

        public TripQueries(TravelContext context, IMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<TripViewModel>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var qry = await (from trip in _context.Trips
                             join driver in _context.Travelers
                                on trip.IdDriver equals driver.Id
                                where ids.Contains(trip.Id)
                             select new TripViewModel
                             {
                                 Id = trip.Id,
                                 Date = trip.Date,
                                 IdDriver = trip.IdDriver,
                                 NameDriver = driver.Name,
                                 Commentary = trip.Commentary
                             })
                              .OrderByDescending(c => c.Date)
                              .ToListAsync();

            return qry;
        }

        public async Task<IList<TripViewModel>> GetPagedAsync(int pageSize = 10, int pageIndex = 0)
        {
            var qry = await (from trip in _context.Trips
                              join driver in _context.Travelers
                                 on trip.IdDriver equals driver.Id
                              select new TripViewModel
                              {
                                  Id = trip.Id,
                                  Date = trip.Date,
                                  IdDriver = trip.IdDriver,
                                  NameDriver = driver.Name,
                                  Commentary = trip.Commentary
                              })
                              .OrderByDescending(c => c.Date)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();

            return qry;
        }

        public async Task<TripViewModel> GetTripByIdAsync(int id)
        {
            var qry = await (from trip in _context.Trips
                             join driver in _context.Travelers
                                on trip.IdDriver equals driver.Id
                             where trip.Id == id
                             select new TripViewModel
                             {
                                 Id = trip.Id,
                                 Date = trip.Date,
                                 IdDriver = trip.IdDriver,
                                 NameDriver = driver.Name,
                                 Commentary = trip.Commentary
                             })
                              .ToAsyncEnumerable()
                              .FirstOrDefault();

            return qry;
        }

        public async Task<long> CountAsync()
        {
            return await _context.Trips.LongCountAsync();
        }
    }
}

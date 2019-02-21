using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.API.ViewModel;
using Travel.Infrastructure;

namespace Travel.API.Infrastructure.Queries
{
    public class TripQueries : ITripQueries
    {
        private readonly TravelContext _context;

        public TripQueries(TravelContext context)
        {
            _context = context;
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

    }
}

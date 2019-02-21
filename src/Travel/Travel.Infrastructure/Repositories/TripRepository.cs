using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Travel.Domain.AggregatesModel.TripAggregate;

namespace Travel.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TravelContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public TripRepository(TravelContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Trip Add(Trip trip)
        {
            return _context.Add(trip).Entity;
        }

        public async Task<Trip> GetByIdAsync(int idTrip)
        {
            return await _context.Trips.FindAsync(idTrip);
        }

        public void Update(Trip trip)
        {
            _context.Entry(trip).State = EntityState.Modified;
        }
    }
}

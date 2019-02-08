using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Travel.Domain.AggregatesModel.TravelAggregate;

namespace Travel.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Trip Add(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> GetByIdAsync(int idTrip)
        {
            throw new NotImplementedException();
        }

        public void Update(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}

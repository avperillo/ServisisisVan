using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.TravelAggregate
{
    public interface ITripRepository : IRepository<Trip>
    {
        Trip Add(Trip trip);
        void Update(Trip trip);
        Task<Trip> GetByIdAsync(int idTrip);
    }
}
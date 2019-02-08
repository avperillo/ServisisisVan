using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.TravelAggregate
{
    public interface ITripRepository : IRepository<Trip>
    {
        Trip Add(Trip charge);
        void Update(Trip charge);
        Task<Trip> GetByIdAsync(int idTravel);
    }
}
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.TripAggregate
{
    public interface ITripService
    {
        Task<Trip> CreateAsync(Trip trip);
        Task<Trip> GetTripByIdAsync(int idTrip);
        Task DeleteAsync(int idTrip);
    }
}

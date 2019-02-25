using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.TravelerAggregate
{
    public interface ITravelerService
    {
        Task<Traveler> GetByIdAsync(int idTraveler);
        Task<Traveler> CreateAsync(Traveler traveler);
        Task<Traveler> UpdateAsync(Traveler traveler);
        Task DeactivateAsync(Traveler traveler);
        Task ReactivateAsync(Traveler traveler);
    }
}

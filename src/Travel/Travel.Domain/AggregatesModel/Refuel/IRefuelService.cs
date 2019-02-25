using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.RefuelAggregate
{
    public interface IRefuelService
    {
        Task<Refuel> GetByIdAsync(int idRefuel);
        Task<Refuel> CreateAsync(Refuel refuel);
        Task<Refuel> UpdateAsync(Refuel refuel);
        Task DeleteAsync(Refuel refuel);
    }
}

using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.RefuelAggregate
{
    public interface IRefuelRepository : IRepository<Refuel>
    {
        Refuel Add(Refuel refuel);
        void Update(Refuel refuel);
        Task<Refuel> GetByIdAsync(int idRefuel);
        void Remove(Refuel refuel);
    }
}

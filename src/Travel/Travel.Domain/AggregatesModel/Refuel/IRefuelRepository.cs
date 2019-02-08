using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.RefuelAggregate
{
    public interface IRefuelRepository : IRepository<Refuel>
    {
        Refuel Add(Refuel charge);
        void Update(Refuel charge);
        Task<Refuel> GetByIdAsync(int idRefuel);
    }
}

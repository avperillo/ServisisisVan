using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.ChargeAggregate
{
    public interface IChargeRepository : IRepository<Charge>
    {
        Charge Add(Charge charge);
        void Update(Charge charge);
        Task<Charge> GetByIdAsync(int idCharge);
    }
}

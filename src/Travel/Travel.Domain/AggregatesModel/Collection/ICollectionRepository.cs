using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public interface IIncomeRepository : IRepository<Collection>
    {
        Collection Add(Collection collection);
        void Update(Collection collection);
        Task<Collection> GetByIdAsync(int idCollection);
    }
}

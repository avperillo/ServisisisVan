using Domain.SeedWork;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Collection Add(Collection collection);
        void Update(Collection collection);
        Task<Collection> GetByIdAsync(int idCollection);
    }
}

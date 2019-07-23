using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public interface ICollectionService
    {
        Task<Collection> Create();
        Task<Collection> NewEntry(int idCollection, DateTime date, int idTraveler, decimal amount);
        Task CloseCollection(int idCollection);
        Task<Collection> GetCollectionAsync(int idCollection);
    }
}

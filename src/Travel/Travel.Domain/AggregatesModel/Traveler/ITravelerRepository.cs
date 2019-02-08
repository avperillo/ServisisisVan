using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.AggregatesModel.TravelerAggregate
{
    public interface ITravelerRepository : IRepository<Traveler>
    {
        Traveler Add(Traveler charge);
        void Update(Traveler charge);
        Task<Traveler> GetByIdAsync(int idTraveler);
    }
}

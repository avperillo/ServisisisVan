using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Travel.Domain.AggregatesModel.RefuelAggregate;

namespace Travel.Infrastructure.Repositories
{
    public class RefuelRepository : IRefuelRepository
    {
        private readonly TravelContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public RefuelRepository(TravelContext context)
        {
            _context = context;
        }

        public Refuel Add(Refuel refuel)
        {
            return _context.Refuels.Add(refuel).Entity;
        }

        public async Task<Refuel> GetByIdAsync(int idRefuel)
        {
            return await _context.Refuels.FindAsync(idRefuel);
        }

        public void Update(Refuel refuel)
        {
            _context.Entry(refuel).State = EntityState.Modified;
        }

        public void Remove(Refuel refuel)
        {
            _context.Refuels.Remove(refuel);
        }
    }
}

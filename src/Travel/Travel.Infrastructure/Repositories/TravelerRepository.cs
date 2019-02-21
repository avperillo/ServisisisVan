using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.Infrastructure.Repositories
{
    public class TravelerRepository : ITravelerRepository
    {
        private readonly TravelContext _context;

        public IUnitOfWork UnitOfWork => _context;
        
        public TravelerRepository(TravelContext context)
        {
            _context = context;
        }

        public Traveler Add(Traveler traveler)
        {
            return _context.Travelers.Add(traveler).Entity;
        }

        public async Task<Traveler> GetByIdAsync(int idTraveler)
        {
            return await _context.Travelers.FindAsync(idTraveler);
        }

        public void Update(Traveler traveler)
        {
            _context.Entry(traveler).State = EntityState.Modified;
        }
    }
}

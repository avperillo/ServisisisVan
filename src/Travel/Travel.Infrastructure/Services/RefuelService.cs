using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.AggregatesModel.RefuelAggregate;

namespace Travel.Infrastructure.Services
{
    public class RefuelService : IRefuelService
    {
        private readonly IRefuelRepository refuelRepository;

        public RefuelService(IRefuelRepository refuelRepository)
        {
            this.refuelRepository = refuelRepository;
        }

        public async Task<Refuel> GetByIdAsync(int idRefuel)
        {
            return await refuelRepository.GetByIdAsync(idRefuel)
                ?? throw new ArgumentException($"The traveler with id '{idRefuel}' doesn´t exist");
        }

        public async Task<Refuel> CreateAsync(Refuel refuel)
        {
            if (refuel.Id != 0)
                throw new ArgumentException("The 'Id' must be different from 0.");

            refuel = refuelRepository.Add(refuel);
            await refuelRepository.UnitOfWork.SaveChangesAsync();

            return refuel;
        }

        public async Task<Refuel> UpdateAsync(Refuel refuel)
        {
            refuelRepository.Update(refuel);
            await refuelRepository.UnitOfWork.SaveChangesAsync();
            return refuel;
        }

        public async Task DeleteAsync(Refuel refuel)
        {
            refuelRepository.Remove(refuel);
            await refuelRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}

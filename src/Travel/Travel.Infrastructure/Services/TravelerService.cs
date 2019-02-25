using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.Infrastructure.Services
{
    public class TravelerService : ITravelerService
    {
        private readonly ITravelerRepository travelerRepository;

        public TravelerService(ITravelerRepository travelerRepository)
        {
            this.travelerRepository = travelerRepository;
        }

        public async Task<Traveler> GetByIdAsync(int idTraveler)
        {
            return await travelerRepository.GetByIdAsync(idTraveler) 
                ?? throw new ArgumentException($"The traveler with id '{idTraveler}' doesn´t exist");
        }

        public async Task<Traveler> CreateAsync(Traveler traveler)
        {
            if (traveler.Id != 0)
                throw new ArgumentException("The 'Id' must be different from 0.");

            traveler = travelerRepository.Add(traveler);
            await travelerRepository.UnitOfWork.SaveChangesAsync();
            return traveler;
        }

        public async Task DeactivateAsync(Traveler traveler)
        {
            traveler.Deactivate();
            travelerRepository.Update(traveler);
            await travelerRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task ReactivateAsync(Traveler traveler)
        {
            traveler.Reactivate();
            travelerRepository.Update(traveler);
            await travelerRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<Traveler> UpdateAsync(Traveler traveler)
        {
            travelerRepository.Update(traveler);
            await travelerRepository.UnitOfWork.SaveChangesAsync();
            return traveler;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.AggregatesModel.CollectionAggregate;
using Travel.Domain.AggregatesModel.TravelerAggregate;
using Travel.Domain.Exceptions;

namespace Travel.Infrastructure.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository collectionRepository;
        private readonly ITravelerRepository travelerRepository;

        public CollectionService(ICollectionRepository collectionRepository,
                                    ITravelerRepository travelerRepository)
        {
            this.collectionRepository = collectionRepository;
            this.travelerRepository = travelerRepository;
        }

        public async Task CloseCollection(int idCollection)
        {
            var collection = await collectionRepository.GetByIdAsync(idCollection);

            CheckStateIsOpen(collection);
            await CheckAllTravelersPayAsync(collection);

            collection.CloseCollection();

            await collectionRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<Collection> Create()
        {
            Collection collection = new Collection(DateTime.Now);

            collectionRepository.Add(collection);
            await collectionRepository.UnitOfWork.SaveChangesAsync();

            return collection;
        }

        public async Task<Collection> NewEntry(int idCollection, DateTime date, int idTraveler, decimal amount)
        {
            var collection = await collectionRepository.GetByIdAsync(idCollection) 
                ?? throw new ArgumentException($"The collection id {idCollection} was not found", nameof(idCollection));

            await CheckExistTravelerAsync(idTraveler);
            CheckStateIsOpen(collection);

            collection.AddEntry(idTraveler, date, amount);
            collectionRepository.Update(collection);
            await collectionRepository.UnitOfWork.SaveChangesAsync();

            return collection;
        }

        public async Task<Collection> GetCollectionAsync(int idCollection)
        {
            Collection collection = await collectionRepository.GetByIdAsync(idCollection);
            return collection;
        }

        private void CheckStateIsOpen(Collection collection)
        {
            if (collection.State == CollectionState.Close)
                throw new ArgumentException($"The collection {0} is closed");
        }

        private async Task CheckAllTravelersPayAsync(Collection collection)
        {
            var travelers = await travelerRepository.GetAllAsync();

            var morosos = from traveler in travelers.ToList()
                          where !collection.Entries.Any(t => t.IdTraveler == traveler.Id)
                          select traveler;

            if (morosos.Any())
                throw new AreMissingTravelersToPayException(string.Join(", ", morosos.Select(m => $"{m.Id} - {m.Name}")));
        }

        private async Task CheckExistTravelerAsync(int idTraveler)
        {
            var travelers = await travelerRepository.GetAllAsync();

            if (!travelers.Any(t => t.Id == idTraveler))
                throw new ArgumentException($"The traveler id [{idTraveler}] was not found.");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.AggregatesModel.TravelerAggregate;
using Travel.Domain.AggregatesModel.TripAggregate;
using Travel.Domain.Exceptions;

namespace Travel.Infrastructure.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository tripRepository;
        private readonly ITravelerRepository travelerRepository;

        public TripService(ITripRepository tripRepository, ITravelerRepository travelerRepository)
        {
            this.tripRepository = tripRepository;
            this.travelerRepository = travelerRepository;
        }

        public async Task<Trip> CreateAsync(Trip trip)
        {
            if (trip.Id == 0)
                throw new ArgumentException("The Id must be 0", nameof(trip.Id));
            
            Traveler traveler = await travelerRepository.GetByIdAsync(trip.IdDriver);
            if (traveler == null)
                throw new TravelerNotExistException($"'{trip.IdDriver}' doesn´t exists");

            if (!traveler.IsDriver)
                throw new TravelerIsNotDriverException($"The traveler '{traveler.Id} - {traveler.Name}' not is a driver");

            trip = tripRepository.Add(trip);
            await tripRepository.UnitOfWork.SaveChangesAsync();

            return trip;
        }

        public Task<Trip> GetTripByIdAsync(int idTrip)
        {
            return tripRepository.GetByIdAsync(idTrip);
        }
    }
}

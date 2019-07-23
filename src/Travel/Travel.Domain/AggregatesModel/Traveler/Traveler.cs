using Domain.SeedWork;
using System;
using Travel.Domain.Events;
using Travel.Domain.Exceptions;

namespace Travel.Domain.AggregatesModel.TravelerAggregate
{
    public class Traveler : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public bool IsDriver { get; private set; }
        public DateTime? LeavingDate { get; private set; } = null;

        public Traveler() { }

        public Traveler(string name, bool isDriver)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException(nameof(name));
            IsDriver = isDriver;
        }

        public void Deactivate()
        {
            if (LeavingDate.HasValue)
                throw new TravelerAlreadyDeactivatedException();

            LeavingDate = DateTime.Now;
            AddDomainEvent(new TravelerDeactivatedDomainEvent(this));
        }

        public void Reactivate()
        {
            if (!LeavingDate.HasValue)
                throw new TravelerAlreadyActivatedException();

            LeavingDate = null;
            AddDomainEvent(new TravelerReactivatedDomainEvent(this));
        }

    }
}

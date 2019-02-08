using Domain.SeedWork;
using System;

namespace Travel.Domain.AggregatesModel.TravelerAggregate
{
    public class Traveler : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public bool IsDriver { get; private set; }

        public Traveler(string name, bool isDriver)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException(nameof(name));
            IsDriver = isDriver;
        }

    }
}

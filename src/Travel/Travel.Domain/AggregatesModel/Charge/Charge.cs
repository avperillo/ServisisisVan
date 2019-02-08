using Domain.SeedWork;
using System;
using Travel.Domain.Events;

namespace Travel.Domain.AggregatesModel.ChargeAggregate
{
    public class Charge : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set; }
        public int IdTraveler { get; private set; }
        public decimal Amount { get; private set; }

        public Charge(DateTime date, int idTraveler, decimal amount)
        {
            Amount = amount > 0 ? amount : throw new ArgumentException(nameof(amount));
            Date = date;
            IdTraveler = idTraveler;

            AddDomainEvent(new ChargeAddedDomainEvent(this));
        }

    }
}

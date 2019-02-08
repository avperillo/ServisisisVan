using Domain.SeedWork;
using System;

namespace Travel.Domain.AggregatesModel.RefuelAggregate
{
    public class Refuel : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set; }

        public decimal Amount { get; private set; }

        public Refuel(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount > 0 ? amount : throw new ArgumentException(nameof(amount));
        }
    }
}

using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.Events;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public class Entry : Entity
    {
        public int IdCollection { get; private set; }
        public DateTime Date { get; private set; }
        public int IdTraveler { get; private set; }
        public decimal Amount { get; private set; }

        public Entry(int idCollection, DateTime date, int idTraveler, decimal amount)
        {
            Amount = amount > 0 ? amount : throw new ArgumentException(nameof(amount));
            IdCollection = idCollection;
            Date = date;
            IdTraveler = idTraveler;

            AddDomainEvent(new EntryAddedDomainEvent(this));
        }
    }
}

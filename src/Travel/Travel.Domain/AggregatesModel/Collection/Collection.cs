using Domain.SeedWork;
using System;
using System.Collections.Generic;
using Travel.Domain.Events;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public class Collection : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set; }
        public CollectionState State { get; private set; }

        private List<Entry> _entries;
        public IReadOnlyCollection<Entry> Entries => _entries;

        public Collection(DateTime date)
        {
            Date = date;
            State = CollectionState.Open;
            _entries = new List<Entry>();

            AddDomainEvent(new CollectionAddedDomainEvent(this));
        }

        public void CloseCollection()
        {
            State = CollectionState.Close;
        }

        public void AddEntry(int idTraveler, DateTime date, decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("The amount must be greater than 0", nameof(amount));

            Entry entry = new Entry(Id, date, idTraveler, amount);
            _entries.Add(entry);
        }

    }
}

using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Travel.Domain.Events;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public class Collection : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set; }

        private int _stateId;
        public CollectionState State { get; private set; }

        private List<Entry> _entries;
        public IReadOnlyCollection<Entry> Entries => _entries;

        public Collection(DateTime date)
        {
            Date = date;
            //State = CollectionState.Open;
            _stateId = CollectionState.Open.Id;
            _entries = new List<Entry>();

            AddDomainEvent(new CollectionAddedDomainEvent(this));
        }

        public void CloseCollection()
        {
            _stateId = CollectionState.Close.Id;
        }

        public void AddEntry(int idTraveler, DateTime date, decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("The amount must be greater than 0", nameof(amount));

            if (_entries.Any(e => e.IdTraveler == idTraveler))
                throw new ArgumentException($"The traveler [{idTraveler}] has already pay.");

            Entry entry = new Entry(date, idTraveler, amount);
            _entries.Add(entry);
        }

    }
}

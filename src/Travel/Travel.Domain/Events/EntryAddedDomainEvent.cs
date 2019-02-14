using MediatR;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Domain.Events
{
    public class EntryAddedDomainEvent : INotification
    {
        Entry Entry { get; }

        public EntryAddedDomainEvent(Entry entry)
        {
            Entry = entry;
        }
    }
}

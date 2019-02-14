using MediatR;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Domain.Events
{
    public class CollectionAddedDomainEvent : INotification
    {
        Collection Collection { get; }

        public CollectionAddedDomainEvent(Collection collection)
        {
            Collection = collection;
        }
    }
}

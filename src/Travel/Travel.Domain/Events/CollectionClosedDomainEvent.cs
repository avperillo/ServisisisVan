using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.Domain.Events
{
    public class CollectionClosedDomainEvent : INotification
    {
        Collection Collection { get; }

        public CollectionClosedDomainEvent(Collection collection)
        {
            this.Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }
    }
}

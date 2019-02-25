using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.Domain.Events
{
    public class TravelerReactivatedDomainEvent : INotification
    {
        private readonly Traveler _traveler;

        public TravelerReactivatedDomainEvent(Traveler traveler)
        {
            _traveler = traveler;
        }
    }
}

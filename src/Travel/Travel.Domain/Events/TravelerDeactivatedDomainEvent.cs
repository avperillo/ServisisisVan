using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.Domain.Events
{
    public class TravelerDeactivatedDomainEvent : INotification
    {
        private readonly Traveler _traveler;

        public TravelerDeactivatedDomainEvent(Traveler traveler)
        {
            _traveler = traveler;
        }
    }
}

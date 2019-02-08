using MediatR;
using Travel.Domain.AggregatesModel.ChargeAggregate;

namespace Travel.Domain.Events
{
    public class ChargeAddedDomainEvent : INotification
    {
        Charge Charge { get; }

        public ChargeAddedDomainEvent(Charge charge)
        {
            Charge = charge;
        }
    }
}

using MediatR;
using Travel.Domain.AggregatesModel.RefuelAggregate;

namespace Travel.Domain.Events
{
    public class RefuelAddedDomainEvent: INotification
    {
        public Refuel Refuel { get; }

        public RefuelAddedDomainEvent(Refuel refuel)
        {
            Refuel = refuel;
        }
    }
}

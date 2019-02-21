using Domain.SeedWork;
using System;

namespace Travel.Domain.AggregatesModel.TripAggregate{
    public class Trip : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set; }

        public int IdDriver { get; private set; }

        public string Commentary { get; set; }


        public Trip(DateTime date, int idDriver)
        {
            Date = date;
            IdDriver = idDriver;
        }


        public void SetDriverId(int idDriver)
        {
            IdDriver = idDriver;
        }

        public void SetDate(DateTime date)
        {
            Date = date;
        }
    }
}

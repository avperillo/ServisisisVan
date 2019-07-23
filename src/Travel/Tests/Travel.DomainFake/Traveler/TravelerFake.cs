using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.TravelerAggregate;

namespace Travel.DomainFake.AggregateTraveler
{
    public class TravelerFake
    {
        public static Traveler GetTraveler(int id)
        {
            var traveler = new Traveler($"Traveler {id}", true);
            traveler.SetPropertyValue(x => x.Id, id);
            return traveler;
        }
        
        public static List<Traveler> GetTravelers(int numberTravelers)
        {
            var travelers = new List<Traveler>();

            for (int i = 0; i < numberTravelers; i++)
                travelers.Add(GetTraveler(i + 1));

            return travelers;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.DomainFake.CollectionAggregate
{
    public class CollectionFake
    {
        public static Collection GetEmptyCollection() => new Collection(DateTime.Today);
        public static Collection GetCloseCollection() => new Collection(DateTime.Today);
    }
}

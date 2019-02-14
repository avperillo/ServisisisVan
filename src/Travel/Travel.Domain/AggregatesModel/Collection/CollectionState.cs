using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Domain.AggregatesModel.CollectionAggregate
{
    public class CollectionState : Enumeration
    {
        public static CollectionState Open = new OpenCollectionState();
        public static CollectionState Close = new CloseCollectionState();

        protected CollectionState(int id, string name) : base(id, name) { }

        private class OpenCollectionState : CollectionState
        {
            public OpenCollectionState() : base(1, "Open") { }
        }

        private class CloseCollectionState : CollectionState
        {
            public CloseCollectionState() : base(2, "Close") { }
        }

    }
}

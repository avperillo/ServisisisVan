using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Domain.Exceptions
{
    [Serializable]
    public class TravelerAlreadyDeactivatedException : Exception
    {
        public TravelerAlreadyDeactivatedException() { }
        public TravelerAlreadyDeactivatedException(string message) : base(message) { }
        public TravelerAlreadyDeactivatedException(string message, Exception inner) : base(message, inner) { }
        protected TravelerAlreadyDeactivatedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Domain.Exceptions
{
    [Serializable]
    public class TravelerAlreadyActivatedException : Exception
    {
        public TravelerAlreadyActivatedException() { }
        public TravelerAlreadyActivatedException(string message) : base(message) { }
        public TravelerAlreadyActivatedException(string message, Exception inner) : base(message, inner) { }
        protected TravelerAlreadyActivatedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

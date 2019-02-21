using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Domain.Exceptions
{
    [Serializable]
    public class TravelerNotExistException : Exception
    {
        public TravelerNotExistException() { }
        public TravelerNotExistException(string message) : base(message) { }
        public TravelerNotExistException(string message, Exception inner) : base(message, inner) { }
        protected TravelerNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

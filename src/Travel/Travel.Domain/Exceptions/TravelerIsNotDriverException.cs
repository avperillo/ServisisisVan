using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Travel.Domain.Exceptions
{
    public class TravelerIsNotDriverException : Exception
    {
        public TravelerIsNotDriverException()
        {
        }

        public TravelerIsNotDriverException(string message) : base(message)
        {
        }

        public TravelerIsNotDriverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TravelerIsNotDriverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

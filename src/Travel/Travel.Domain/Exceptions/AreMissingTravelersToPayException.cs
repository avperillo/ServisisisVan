using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Travel.Domain.Exceptions
{
    public class AreMissingTravelersToPayException : Exception
    {
        public AreMissingTravelersToPayException()
        {
        }

        public AreMissingTravelersToPayException(string message) : base(message)
        {
        }

        public AreMissingTravelersToPayException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AreMissingTravelersToPayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

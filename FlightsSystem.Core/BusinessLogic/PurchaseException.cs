using System;
using System.Runtime.Serialization;

namespace FlightsSystem.Core.BusinessLogic
{
    public class PurchaseException : Exception
    {

        public PurchaseException()
        {
        }

        public PurchaseException(string message) : base(message)
        {
        }

        public PurchaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PurchaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
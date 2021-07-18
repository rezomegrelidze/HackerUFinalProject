using System;
using System.Runtime.Serialization;

namespace FlightsSystem.Core.BusinessLogic
{
    public class TokenNotValidException : Exception
    {
        public TokenNotValidException()
        {
        }

        public TokenNotValidException(string message) : base(message)
        {
        }

        public TokenNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
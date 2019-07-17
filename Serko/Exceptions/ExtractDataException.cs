using System;
using System.Runtime.Serialization;

namespace Serko.Exceptions
{
    public class ExtractDataException : Exception
    {
        public ExtractDataException()
        {
        }

        public ExtractDataException(string message)
            : base(message)
        {
        }

        public ExtractDataException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ExtractDataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

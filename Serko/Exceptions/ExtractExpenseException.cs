using System;
using System.Runtime.Serialization;

namespace Serko.Exceptions
{
    public class ExtractExpenseException : Exception
    {
        public ExtractExpenseException()
        {
        }

        public ExtractExpenseException(string message)
            : base(message)
        {
        }

        public ExtractExpenseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ExtractExpenseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
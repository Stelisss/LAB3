using System;
using System.Runtime.Serialization;

namespace ConsoleApp1
{
    [Serializable]
    internal class message : Exception
    {
        public message()
        {
        }

        public message(string message) : base(message)
        {
        }

        public message(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected message(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
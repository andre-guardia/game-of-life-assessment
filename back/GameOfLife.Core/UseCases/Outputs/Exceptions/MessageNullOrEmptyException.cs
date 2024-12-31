using System.Runtime.Serialization;

namespace GameOfLife.Core.UseCases.Outputs.Exceptions
{
    [Serializable]
    public class MessageNullOrEmptyException : Exception
    {
        public MessageNullOrEmptyException(string message) : base(message)
        {
        }

        public MessageNullOrEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MessageNullOrEmptyException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }
    }
}

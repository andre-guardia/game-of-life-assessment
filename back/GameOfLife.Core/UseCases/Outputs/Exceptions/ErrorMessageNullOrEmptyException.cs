using System.Runtime.Serialization;

namespace GameOfLife.Core.UseCases.Outputs.Exceptions
{
    [Serializable]
    public class ErrorMessageNullOrEmptyException : Exception
    {
        public ErrorMessageNullOrEmptyException(string message) : base(message)
        {
        }

        public ErrorMessageNullOrEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ErrorMessageNullOrEmptyException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }
    }
}

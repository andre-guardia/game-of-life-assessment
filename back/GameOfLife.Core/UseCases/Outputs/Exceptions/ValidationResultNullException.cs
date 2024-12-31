using System.Runtime.Serialization;

namespace GameOfLife.Core.UseCases.Outputs.Exceptions
{
    [Serializable]
    public class ValidationResultNullException : Exception
    {
        public ValidationResultNullException(string message) : base(message)
        {
        }

        public ValidationResultNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ValidationResultNullException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }
    }
}

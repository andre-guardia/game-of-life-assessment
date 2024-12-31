using System.Runtime.Serialization;

namespace GameOfLife.Core.Api.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DomainException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }
    }
}

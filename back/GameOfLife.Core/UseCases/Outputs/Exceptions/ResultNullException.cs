using System.Runtime.Serialization;

namespace GameOfLife.Core.UseCases.Outputs.Exceptions
{
    [Serializable]
    public class ResultNullException : Exception
    {
        public ResultNullException(string message) : base(message)
        {
        }

        public ResultNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ResultNullException(SerializationInfo info,
        StreamingContext context) : base(info, context) { }
    }
}

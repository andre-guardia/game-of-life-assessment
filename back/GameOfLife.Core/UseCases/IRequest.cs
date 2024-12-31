namespace GameOfLife.Core.UseCases
{
    public interface IRequest
    {
        public Guid CorrelationId { get; set; }
        public Guid TransactionId { get; set; }
    }

    public interface IRequest<T> : IRequest
    {
    }
}

using GameOfLife.Core.UseCases.Outputs;

namespace GameOfLife.Core.UseCases
{
    public interface IUseCase<in TInput, TOutput>
        where TInput : notnull, IRequest<TOutput>
        where TOutput : notnull
    {
        public Task<Output<TOutput>> ExecuteAsync(TInput request, CancellationToken cancellationToken);
    }

    public interface IUseCase<in TInput>
      where TInput : notnull, IRequest
    {
        public Task<Output> ExecuteAsync(TInput request, CancellationToken cancellationToken);
    }
}

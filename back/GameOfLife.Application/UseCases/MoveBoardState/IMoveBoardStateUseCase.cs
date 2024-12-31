using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.MoveBoardState
{
    public interface IMoveBoardStateUseCase : IRequestHandler<MoveBoardStateInput, Output>
    {
    }
}

using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.RestartBoard
{
    public interface IRestartBoardUseCase : IRequestHandler<RestartBoardInput, Output>
    {
    }
}

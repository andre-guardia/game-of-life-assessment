using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.GetBoard
{
    public interface IGetBoardUseCase : IRequestHandler<GetBoardInput, Output>
    {
    }
}

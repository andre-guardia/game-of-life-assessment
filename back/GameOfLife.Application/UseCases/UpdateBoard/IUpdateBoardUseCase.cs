using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.UpdateBoard
{
    public interface IUpdateBoardUseCase : IRequestHandler<UpdateBoardInput, Output>
    {
    }
}

using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.CreateBoard
{
    public interface ICreateBoardUseCase : IRequestHandler<CreateBoardInput, Output>
    {
    }
}

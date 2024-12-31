using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.RestartBoard
{
    public class RestartBoardInput : IRequest<Output>
    {
        public RestartBoardInput(Guid boardId)
        {
            BoardId = boardId;
        }

        public Guid BoardId { get; set; }
    }
}

using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.GetBoard
{
    public class GetBoardInput : IRequest<Output>
    {
        public GetBoardInput(Guid boardId)
        {
            BoardId = boardId;
        }

        public Guid BoardId { get; set; }
    }
}

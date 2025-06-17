using GameOfLife.Application.UseCases.Shared;
using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.MoveBoardState
{
    public class MoveBoardStateInput : IRequest<Output>
    {
        public Guid BoardId { get; set; }
        public int? Lenght { get; set; }
        public Direction Direction { get; set; }
        public bool FinalState { get; set; }
    }
}

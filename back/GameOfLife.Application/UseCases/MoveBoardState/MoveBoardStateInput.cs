using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.MoveBoardState
{
    public class MoveBoardStateInput : IRequest<Output>
    {
        public Guid BoardId { get; set; }
        public int? Lenght { get; set; }
        public bool FinalState { get; set; }
    }
}

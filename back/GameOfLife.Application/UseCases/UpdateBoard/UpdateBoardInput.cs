using GameOfLife.Application.UseCases.Shared.Inputs;
using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.UpdateBoard
{
    public class UpdateBoardInput : IRequest<Output>
    {
        public Guid BoardId { get; set; }
        public List<CellInput> Cells { get; set; } = new List<CellInput>();
    }
}

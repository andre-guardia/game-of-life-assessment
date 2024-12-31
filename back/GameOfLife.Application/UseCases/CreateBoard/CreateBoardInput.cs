using GameOfLife.Application.UseCases.Shared.Inputs;
using GameOfLife.Core.UseCases.Outputs;
using MediatR;

namespace GameOfLife.Application.UseCases.CreateBoard
{
    public class CreateBoardInput : IRequest<Output>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool RandomCells { get; set; }
        public List<CellInput>? Cells { get; set; }
    }
}

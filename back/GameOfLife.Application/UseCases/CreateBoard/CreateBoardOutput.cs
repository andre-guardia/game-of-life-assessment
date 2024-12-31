using GameOfLife.Application.UseCases.Shared.Outputs;

namespace GameOfLife.Application.UseCases.CreateBoard
{
    public class CreateBoardOutput
    {
        public BoardOutput Data { get; set; }

        public CreateBoardOutput(BoardOutput data)
        {
            Data = data;
        }
    }
}

using GameOfLife.Application.UseCases.Shared.Outputs;

namespace GameOfLife.Application.UseCases.GetBoard
{
    public class GetBoardOutput
    {
        public GetBoardOutput(BoardOutput data)
        {
            Data = data;
        }

        public BoardOutput Data { get; set; }
    }
}

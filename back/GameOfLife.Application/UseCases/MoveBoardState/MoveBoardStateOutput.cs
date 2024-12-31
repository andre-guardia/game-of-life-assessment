using GameOfLife.Application.UseCases.Shared.Outputs;

namespace GameOfLife.Application.UseCases.MoveBoardState
{
    public class MoveBoardStateOutput
    {
        public MoveBoardStateOutput(BoardOutput data)
        {
            Data = data;
        }

        public BoardOutput Data { get; set; }
    }
}

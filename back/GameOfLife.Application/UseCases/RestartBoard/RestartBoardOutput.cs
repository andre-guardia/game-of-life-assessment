using GameOfLife.Application.UseCases.Shared.Outputs;

namespace GameOfLife.Application.UseCases.RestartBoard
{
    public class RestartBoardOutput
    {
        public bool Success { get; set; }
        public BoardOutput Data { get; set; }

        public RestartBoardOutput(bool success, BoardOutput data)
        {
            Success = success;
            Data = data;
        }
    }
}

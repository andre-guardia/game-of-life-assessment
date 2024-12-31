using AutoMapper;
using GameOfLife.Application.UseCases.Shared.Outputs;
using GameOfLife.Core.UseCases.Outputs;
using GameOfLife.Domain.Contexts;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.UseCases.RestartBoard
{
    public class RestartBoard : IRestartBoardUseCase
    {
        private readonly ILogger<RestartBoard> _logger;
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public RestartBoard(ILogger<RestartBoard> logger, IBoardRepository boardRepository, IMapper mapper)
        {
            _logger = logger;
            _boardRepository = boardRepository;
            _mapper = mapper;
        }

        public async Task<Output> Handle(RestartBoardInput request, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.GetBoardByIdAsync(request.BoardId);
            if (board is null)
            {
                _logger.LogInformation($"Board {request.BoardId} not found");
                return new Output();
            }

            board.RestartBoard();
            await _boardRepository.UpdateBoardAsync(board);
            return new Output(new RestartBoardOutput(true, _mapper.Map<BoardOutput>(board)));
        }
    }
}

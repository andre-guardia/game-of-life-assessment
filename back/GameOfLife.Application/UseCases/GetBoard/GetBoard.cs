using AutoMapper;
using GameOfLife.Application.UseCases.Shared.Outputs;
using GameOfLife.Core.UseCases.Outputs;
using GameOfLife.Domain.Contexts;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.UseCases.GetBoard
{
    public class GetBoard : IGetBoardUseCase
    {
        private readonly ILogger<GetBoard> _logger;
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public GetBoard(IBoardRepository boardRepository, ILogger<GetBoard> logger, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Output> Handle(GetBoardInput request, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.GetBoardByIdAsync(request.BoardId);
            if (board == null)
            {
                _logger.LogInformation($"Board {request.BoardId} not found");
                return new Output();
            }

            return new Output(new GetBoardOutput(_mapper.Map<BoardOutput>(board)));
        }
    }
}

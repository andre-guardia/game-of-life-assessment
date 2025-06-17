using AutoMapper;
using GameOfLife.Application.UseCases.Shared;
using GameOfLife.Core.UseCases.Outputs;
using GameOfLife.Domain.Contexts;
using GameOfLife.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.UseCases.UpdateBoard
{
    public class UpdateBoard : BaseUseCase<UpdateBoard>, IUpdateBoardUseCase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public UpdateBoard(ILogger<UpdateBoard> logger, IBoardRepository boardRepository, IMapper mapper) : base(logger)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
        }

        public async Task<Output> Handle(UpdateBoardInput request, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.GetBoardByIdAsync(request.BoardId);
            if (board is null)
                return HandleError($"Board {request.BoardId} not found");

            var cells = request.Cells.Select(_mapper.Map<Cell>);
            board.SetCells(cells);

            return new Output();
        }
    }
}

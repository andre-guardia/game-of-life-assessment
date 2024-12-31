using AutoMapper;
using GameOfLife.Application.UseCases.Shared.Outputs;
using GameOfLife.Core.UseCases.Outputs;
using GameOfLife.Domain.Contexts;
using GameOfLife.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.UseCases.CreateBoard
{
    public class CreateBoard : ICreateBoardUseCase
    {
        private readonly ILogger<CreateBoard> _logger;
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public CreateBoard(ILogger<CreateBoard> logger, IBoardRepository boardRepository, IMapper mapper)
        {
            _logger = logger;
            _boardRepository = boardRepository;
            _mapper = mapper;
        }

        public async Task<Output> Handle(CreateBoardInput request, CancellationToken cancellationToken)
        {
            var board = MapBoard(request);
            await _boardRepository.AddBoardAsync(board);
            return new Output(new CreateBoardOutput(_mapper.Map<BoardOutput>(board)));
        }

        private Board MapBoard(CreateBoardInput request)
        {
            var cells = (request.Cells is not null ? request.Cells.Select(_mapper.Map<Cell>) : new List<Cell>()).ToList();
            return Board.Create(10, 10, cells, request.RandomCells);
        }
    }
}

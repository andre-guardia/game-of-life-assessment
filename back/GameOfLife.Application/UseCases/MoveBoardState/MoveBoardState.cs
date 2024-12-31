﻿using AutoMapper;
using GameOfLife.Application.UseCases.Shared;
using GameOfLife.Application.UseCases.Shared.Outputs;
using GameOfLife.Core.UseCases.Outputs;
using GameOfLife.Domain.Contexts;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.UseCases.MoveBoardState
{
    public class MoveBoardState : BaseUseCase<MoveBoardState>, IMoveBoardStateUseCase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public MoveBoardState(ILogger<MoveBoardState> logger, IBoardRepository boardRepository, IMapper mapper) : base(logger)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
        }

        public async Task<Output> Handle(MoveBoardStateInput request, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.GetBoardByIdAsync(request.BoardId);
            if (board is null)
                return HandleError($"Board {request.BoardId} not found");

            if (board.IsCrashed)
                return HandleError($"Board {request.BoardId} is crashed");

            if (request.FinalState)
            {
                while (!board.IsCrashed)
                    board.MoveNext();
            }
            if (request.Lenght.HasValue)
            {
                for (var i = 0; i < request.Lenght.Value; i++)
                {
                    if (!board.IsCrashed)
                        board.MoveNext();
                }
            }
            else
                board.MoveNext();

            return new Output(new MoveBoardStateOutput(_mapper.Map<BoardOutput>(board)));
        }
    }
}
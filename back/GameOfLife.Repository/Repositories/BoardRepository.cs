using GameOfLife.Domain.Contexts;
using GameOfLife.Domain.Entities;
using GameOfLife.Repository.Contexts;

namespace GameOfLife.Repository.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly BoardContext _context;

        public BoardRepository(BoardContext context)
        {
            _context = context;
        }

        public Task<Board?> GetBoardByIdAsync(Guid id)
        {
            var board = _context.GetBoardById(id);
            return Task.FromResult(board);
        }

        public Task AddBoardAsync(Board board)
        {
            _context.AddBoard(board);
            return Task.CompletedTask;
        }

        public Task UpdateBoardAsync(Board board)
        {
            _context.UpdateBoard(board);
            return Task.CompletedTask;
        }

        public Task RemoveBoardAsync(Guid id)
        {
            _context.RemoveBoard(id);
            return Task.CompletedTask;
        }
    }
}

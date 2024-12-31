using GameOfLife.Domain.Entities;

namespace GameOfLife.Domain.Contexts
{
    public interface IBoardRepository
    {
        Task<Board?> GetBoardByIdAsync(Guid id);
        Task AddBoardAsync(Board board);
        Task UpdateBoardAsync(Board board);
        Task RemoveBoardAsync(Guid id);
    }
}

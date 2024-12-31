using GameOfLife.Domain.Entities;

namespace GameOfLife.Repository.Contexts
{
    public class BoardContext
    {
        private Dictionary<Guid, Board> _boards;

        public BoardContext()
        {
            _boards = new Dictionary<Guid, Board>();
        }

        public Board? GetBoardById(Guid id)
        {
            _boards.TryGetValue(id, out var board);
            return board;
        }

        public void AddBoard(Board board)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            _boards.Add(board.Id, board);
        }

        public void UpdateBoard(Board board)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (_boards.ContainsKey(board.Id))
            {
                _boards[board.Id] = board;
            }
        }

        public void RemoveBoard(Guid id)
        {
            _boards.Remove(id);
        }
    }
}

namespace GameOfLife.Domain.Entities
{
    public class Cell
    {
        public Cell(int x, int y, bool alive)
        {
            X = x;
            Y = y;
            Alive = alive;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Alive { get; set; }

        public int GetAliveNeighbors(List<Cell> allCells)
        {
            var neighbors = new List<Cell?>
            {
                allCells.FirstOrDefault(c => c.Alive && c.X == X && c.Y == Y + 1),
                allCells.FirstOrDefault(c => c.Alive && c.X == X - 1 && c.Y == Y + 1),
                allCells.FirstOrDefault(c => c.Alive && c.X == X + 1 && c.Y == Y + 1),
                allCells.FirstOrDefault(c => c.Alive && c.X == X - 1 && c.Y == Y),
                allCells.FirstOrDefault(c => c.Alive && c.X == X + 1 && c.Y == Y),
                allCells.FirstOrDefault(c => c.Alive && c.X == X && c.Y == Y - 1),
                allCells.FirstOrDefault(c => c.Alive && c.X == X - 1 && c.Y == Y - 1),
                allCells.FirstOrDefault(c => c.Alive && c.X == X + 1 && c.Y == Y - 1)
            };

            return neighbors.Count(n => n != null);
        }
    }
}

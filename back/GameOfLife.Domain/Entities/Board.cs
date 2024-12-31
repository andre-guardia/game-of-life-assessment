namespace GameOfLife.Domain.Entities
{
    public class Board
    {
        public Board(Guid id, int width, int height, List<Cell> cells)
        {
            Id = id;
            Width = width;
            Height = height;
            Cells = cells;
            PreviousStates = new List<Board>();
        }

        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Cell> Cells { get; private set; }
        public List<Board> PreviousStates { get; private set; }
        public int StateCount => PreviousStates.Count;
        public int AliveCount => Cells.Count(c => c.Alive);
        public bool IsCrashed
        {
            get
            {
                if (PreviousStates.Count < 3)
                    return false;

                for (int i = 1; i <= 2; i++)
                {
                    var previous = PreviousStates[PreviousStates.Count - i];
                    if (previous.Equals(this))
                        return true;
                }

                return false;
            }
        }

        public static Board Create(int width, int height, List<Cell> aliveCells, bool randomActive)
        {
            var cells = new List<Cell>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bool active = randomActive && (new Random().Next() > (int.MaxValue / 2));
                    cells.Add(new(x, y, active));
                }
            }

            foreach (var cell in aliveCells)
            {
                var found = cells.FirstOrDefault(c => c.X == cell.X && c.Y == cell.Y);
                if (found != null)
                    found.Alive = true;
                else
                    throw new Exception("Cell not found (check board dimensions)");
            }

            return new Board(Guid.NewGuid(), width, height, cells);
        }

        public void MoveNext()
        {
            List<Cell> nextCells = new List<Cell>();

            foreach (var cell in Cells)
            {
                var aliveNeigbors = cell.GetAliveNeighbors(Cells);
                var newCell = new Cell(cell.X, cell.Y, false);

                if (cell.Alive)
                {
                    if (aliveNeigbors == 2 || aliveNeigbors == 3)
                        newCell.Alive = true;
                }
                else
                {
                    if (aliveNeigbors == 3)
                        newCell.Alive = true;
                }

                nextCells.Add(newCell);
            }

            PreviousStates.Add(new Board(Id, Width, Height, Cells));
            Cells = nextCells;
        }

        public void RestartBoard()
        {
            var original = PreviousStates.FirstOrDefault();
            if (original is not null)
            {
                Cells = original.Cells.ToList();
                PreviousStates.Clear();
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var other = obj as Board;
            if (other == null)
                throw new ArgumentException(nameof(obj));

            foreach (var cell in other.Cells)
            {
                var current = Cells.FirstOrDefault(c => c.X == cell.X && c.Y == cell.Y);
                if (current is null)
                    return false;

                if (!current.Alive.Equals(cell.Alive))
                    return false;
            }

            return true;
        }
    }
}

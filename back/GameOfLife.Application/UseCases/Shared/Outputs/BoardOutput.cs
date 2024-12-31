namespace GameOfLife.Application.UseCases.Shared.Outputs
{
    public class BoardOutput
    {
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<CellOutput> Cells { get; set; }
        public int StateCount { get; set; }
        public bool IsCrashed { get; set; }
    }
}

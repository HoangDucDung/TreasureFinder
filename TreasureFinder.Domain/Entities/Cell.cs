namespace TreasureFinder.Domain.Entities
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Value { get; set; }

        public double DistanceTo(Cell other)
        {
            int dx = Row - other.Row;
            int dy = Col - other.Col;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

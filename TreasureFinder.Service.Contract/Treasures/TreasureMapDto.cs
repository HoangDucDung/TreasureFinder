namespace TreasureFinder.Service.Contract.Treasures
{
    public class TreasureMapDto
    {
        public Guid Id { get; set; }
        public int NumRows { get; set; }
        public int NumCols { get; set; }
        public int NumChestTypes { get; set; }
        public double MinimumFuel { get; set; }
        public string OptimalPath { get; set; } = string.Empty;
        public ICollection<IslandDto> Islands { get; set; } = new List<IslandDto>();
    }
}

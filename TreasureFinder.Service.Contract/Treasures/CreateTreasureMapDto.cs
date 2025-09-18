namespace TreasureFinder.Service.Contract.Treasures
{
    public class CreateTreasureMapDto
    {
        public int NumRows { get; set; }
        public int NumCols { get; set; }
        public int NumChestTypes { get; set; }
        public List<IslandDto> Islands { get; set; } = new List<IslandDto>();
    }
}

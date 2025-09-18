namespace TreasureFinder.Domain.Entities
{
    public class Island
    {
        public int RowPosition { get; set; }
        public int ColPosition { get; set; }
        public int ChestNumber { get; set; }
        public Guid MapId { get; set; }
    }
}

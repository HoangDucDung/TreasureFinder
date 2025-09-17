namespace TreasureFinder.Service.Treasures
{
    public class MatrixDto
    {
        public int Rows { get; }
        public int Cols { get; }
        public CellDto[,] Cells { get; }
    }
}

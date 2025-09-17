namespace TreasureFinder.Domain.Entities
{
    public class Matrix
    {
        public int Rows { get; }
        public int Cols { get; }
        public Cell[,] Cells { get; }

        public Matrix(int rows, int cols, int[,] values)
        {
            Rows = rows;
            Cols = cols;
            Cells = new Cell[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    Cells[i, j] = new Cell(i, j, values[i, j]);
        }

        public IEnumerable<Cell> GetCellsByValue(int value) =>
            Cells.Cast<Cell>().Where(c => c.Value == value);
    }
}

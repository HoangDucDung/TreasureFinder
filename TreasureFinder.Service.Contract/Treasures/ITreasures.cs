using TreasureFinder.Service.Treasures;

namespace TreasureFinder.Service.Contract.Treasures
{
    public interface ITreasures
    {
        /// <summary>
        /// Tìm đường đi tối ưu theo thứ tự: 1 -> 2 -> ... -> p
        /// </summary>
        (double Cost, List<CellDto> Path) Execute(MatrixDto matrix, int p);
    }
}

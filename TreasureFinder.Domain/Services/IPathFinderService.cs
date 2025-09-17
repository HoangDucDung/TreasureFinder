using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureFinder.Domain.Entities;

namespace TreasureFinder.Domain.Services
{
    public interface IPathFinderService
    {
        /// <summary>
        /// Tìm đường đi tối ưu theo thứ tự: 1 -> 2 -> ... -> p
        /// </summary>
        (double Cost, List<Cell> Path) FindShortestPath(Matrix matrix, int p);
    }
}

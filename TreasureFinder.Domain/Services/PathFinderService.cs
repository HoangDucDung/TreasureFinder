using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureFinder.Domain.Entities;

namespace TreasureFinder.Domain.Services
{
    public class PathFinderService : IPathFinderService
    {
        public (double Cost, List<Cell> Path) FindShortestPath(Matrix matrix, int p)
        {
            List<Cell> path = new();
            double totalCost = 0;

            // Bắt đầu từ vị trí chứa p (kho báu) nhưng chưa mở được
            Cell start = matrix.GetCellsByValue(p).First();

            Cell current = start;
            for (int key = 1; key <= p; key++)
            {
                var nextCells = matrix.GetCellsByValue(key);
                // tìm ô gần nhất theo khoảng cách Euclid
                var best = nextCells
                    .Select(c => new { Cell = c, Dist = current.DistanceTo(c) })
                    .OrderBy(x => x.Dist)
                    .First();

                totalCost += best.Dist;
                path.Add(best.Cell);
                current = best.Cell;
            }

            return (totalCost, path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureFinder.Domain.Entities;
using TreasureFinder.Domain.Services;
using TreasureFinder.Service.Contract.Treasures;

namespace TreasureFinder.Service.Treasures
{
    public class Treasures : ITreasures
    {
        private readonly IPathFinderService _pathFinder;
        public Treasures(IPathFinderService pathFinder)
        {
            _pathFinder = pathFinder;
        }
        public (double Cost, List<CellDto> Path) Execute(MatrixDto matrix, int p)
        {
            return (0, []);
        }
    }
}

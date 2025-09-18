using TreasureFinder.Domain.Entities;

namespace TreasureFinder.Domain.Repositories
{
    public interface ITreasureMapRepository
    {
        Task<IEnumerable<TreasureMap>> GetAllMapsAsync();
        Task<TreasureMap> GetMapByIdAsync(Guid id);
        Task<TreasureMap> GetMapWithIslandsAsync(Guid id);
        Task<Guid> CreateMapAsync(TreasureMap map);
        Task<bool> UpdateMapRouteAsync(Guid id, double minimumFuel, string optimalPath);
        Task<bool> SaveIslandsForMapAsync(Guid mapId, IEnumerable<Island> islands);
    }
}

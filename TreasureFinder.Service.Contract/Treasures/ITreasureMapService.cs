using TreasureFinder.Service.Contract.Treasures;

namespace TreasureFinder.Service.Contract.Treasures
{
    public interface ITreasureMapService
    {
        Task<IEnumerable<TreasureMapDto>> GetAllMapsAsync();
        Task<TreasureMapDto?> GetMapByIdAsync(Guid id);
        Task<Guid> CreateTreasureMapAsync(CreateTreasureMapDto createDto);
        Task<bool> RecalculateRouteAsync(Guid mapId);
    }
}

using AutoMapper;
using TreasureFinder.Domain.Entities;
using TreasureFinder.Domain.Repositories;
using TreasureFinder.Service.Contract.Treasures;

namespace TreasureFinder.Service.Treasures
{
    public class TreasureMapService : ITreasureMapService
    {
        private readonly ITreasureMapRepository _treasureMapRepository;
        private readonly IMapper _mapper;

        public TreasureMapService(ITreasureMapRepository treasureMapRepository, IMapper mapper)
        {
            _treasureMapRepository = treasureMapRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TreasureMapDto>> GetAllMapsAsync()
        {
            var maps = await _treasureMapRepository.GetAllMapsAsync();
            return _mapper.Map<IEnumerable<TreasureMapDto>>(maps);
        }

        public async Task<TreasureMapDto?> GetMapByIdAsync(Guid id)
        {
            var map = await _treasureMapRepository.GetMapByIdAsync(id);
            if (map == null) return null;
            return _mapper.Map<TreasureMapDto>(map);
        }

        public async Task<Guid> CreateTreasureMapAsync(CreateTreasureMapDto createDto)
        {
            var map = _mapper.Map<TreasureMap>(createDto);
            map.CalculateTreasureRoute(map);
            var mapId = await _treasureMapRepository.CreateMapAsync(map);

            var islands = _mapper.Map<IEnumerable<Island>>(createDto.Islands);
            await _treasureMapRepository.SaveIslandsForMapAsync(mapId, islands);

            return mapId;
        }

        public async Task<bool> RecalculateRouteAsync(Guid mapId)
        {
            var map = await _treasureMapRepository.GetMapWithIslandsAsync(mapId);

            if (map == null) return false;

            map.CalculateTreasureRoute(map);

            await _treasureMapRepository.UpdateMapRouteAsync(mapId, map.MinimumFuel, map.OptimalPath);

            return map.MinimumFuel == 0 && string.IsNullOrEmpty(map.OptimalPath);
        }
    }
}

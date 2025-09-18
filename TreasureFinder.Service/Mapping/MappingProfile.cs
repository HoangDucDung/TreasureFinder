using AutoMapper;
using TreasureFinder.Domain.Entities;
using TreasureFinder.Service.Contract.Treasures;

namespace TreasureFinder.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TreasureMap, TreasureMapDto>();
            CreateMap<TreasureMapDto, TreasureMap>();
            CreateMap<CreateTreasureMapDto, TreasureMap>();
            CreateMap<TreasureMap, CreateTreasureMapDto>();
            CreateMap<IslandDto, Island>();
            CreateMap<Island, IslandDto>();
        }
    }
}
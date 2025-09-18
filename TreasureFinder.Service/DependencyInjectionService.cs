using Microsoft.Extensions.DependencyInjection;
using TreasureFinder.Service.Contract.Treasures;
using TreasureFinder.Service.Treasures;

namespace TreasureFinder.Service
{
    public static class DependencyInjectionService
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITreasureMapService, TreasureMapService>();
        }
    }
}

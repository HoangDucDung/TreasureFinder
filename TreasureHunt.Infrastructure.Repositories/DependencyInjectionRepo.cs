using Microsoft.Extensions.DependencyInjection;
using TreasureFinder.Domain.Repositories;
using TreasureHunt.Infrastructure.Repositories.Data;
using TreasureHunt.Infrastructure.Repositories.Repositories;

namespace TreasureHunt.Infrastructure.Repositories
{
    public static class DependencyInjectionRepo
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ConnectionFactory>();
            services.AddScoped<ITreasureMapRepository, TreasureMapRepository>();
        }
    }
}

using System.Data;
using Dapper;
using TreasureFinder.Domain.Entities;
using TreasureFinder.Domain.Repositories;
using TreasureHunt.Infrastructure.Repositories.Data;

namespace TreasureHunt.Infrastructure.Repositories.Repositories
{
    public class TreasureMapRepository : ITreasureMapRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        public TreasureMapRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TreasureMap>> GetAllMapsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<TreasureMap>(
                "SELECT Id, NumRows, NumCols, NumChestTypes, MinimumFuel, OptimalPath FROM MapConfiguration");
        }

        public async Task<TreasureMap> GetMapWithIslandsAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();

            var map = await connection.QuerySingleOrDefaultAsync<TreasureMap>(
                "SELECT Id, NumRows, NumCols, NumChestTypes, MinimumFuel, OptimalPath FROM MapConfiguration WHERE Id = @Id",
                new { Id = id });

            if (map != null)
            {
                var islands = await connection.QueryAsync<Island>(
                    @"SELECT RowPosition, ColPosition, ChestNumber FROM Islands WHERE MapId = @MapId",
                    new { MapId = id });

                map.Islands = islands.ToList();
            }
            return map;
        }

        public async Task<Guid> CreateMapAsync(TreasureMap map)
        {
            using var connection = _connectionFactory.CreateConnection();
            var newId = Guid.NewGuid();
            await connection.ExecuteAsync(
                @"INSERT INTO MapConfiguration (Id, NumRows, NumCols, NumChestTypes, MinimumFuel, OptimalPath) 
                VALUES (@Id, @NumRows, @NumCols, @NumChestTypes, @MinimumFuel, @OptimalPath)",
                new { Id = newId, map.NumRows, map.NumCols, map.NumChestTypes, map.MinimumFuel, map.OptimalPath });
            return newId;
        }

        public async Task<bool> UpdateMapRouteAsync(Guid id, double minimumFuel, string optimalPath)
        {
            using var connection = _connectionFactory.CreateConnection();
            var affected = await connection.ExecuteAsync(
                @"UPDATE MapConfiguration SET 
                MinimumFuel = @MinimumFuel, 
                OptimalPath = @OptimalPath
                WHERE Id = @Id",
                new { Id = id, MinimumFuel = minimumFuel, OptimalPath = optimalPath });

            return affected > 0;
        }

        public async Task<bool> SaveIslandsForMapAsync(Guid mapId, IEnumerable<Island> islands)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Delete existing islands for this map
                await connection.ExecuteAsync(
                    "DELETE FROM Islands WHERE MapId = @MapId",
                    new { MapId = mapId },
                    transaction);

                // Insert new islands
                var islandsWithMapId = islands.Select(i => new
                {
                    MapId = mapId,
                    i.RowPosition,
                    i.ColPosition,
                    i.ChestNumber
                });

                await connection.ExecuteAsync(
                    @"INSERT INTO Islands (MapId, RowPosition, ColPosition, ChestNumber) 
                    VALUES (@MapId, @RowPosition, @ColPosition, @ChestNumber)",
                    islandsWithMapId,
                    transaction);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
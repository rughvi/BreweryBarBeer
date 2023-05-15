using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public class BreweryBeerRepository : IBreweryBeerRepository
    {
        private readonly ILogger<BreweryBeerRepository> _logger;
        private readonly DapperContext _context;
        public BreweryBeerRepository(ILogger<BreweryBeerRepository> logger, DapperContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Create(BreweryBeerDTO breweryBeerDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryBeerRepository)}");
            var query = @"IF EXISTS (SELECT * FROM Brewery WHERE Id = @BreweryId ) 
                            AND EXISTS (SELECT * FROM Beer WHERE Id = @BeerId)
                            BEGIN
                                INSERT INTO BreweryBeer (BreweryId, BeerId) VALUES (@BreweryId, @BeerId)
                            END";
            var parameters = new DynamicParameters();
            parameters.Add("BreweryId", breweryBeerDTO.BreweryId, DbType.Int32);
            parameters.Add("BeerId", breweryBeerDTO.BeerId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var result = await connection.ExecuteAsync(query, parameters);
                    if (result == -1)
                    {
                        //Nothing gets updated. 
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<BreweryBeerDTO>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryBeerRepository)}");
            var query = @"SELECT bb.Id as Id, b.Id as BreweryId, b.Name as BreweryName, be.Id as BeerId, be.Name as BeerName, be.PercentageAlcoholByVolume as PercentageAlcoholByVolume
                            FROM Brewery b 
                            LEFT JOIN BreweryBeer bb ON b.Id = bb.BreweryId
                            LEFT JOIN Beer be on be.Id = bb.BeerId";

            using (var connection = _context.CreateConnection())
            {
                var breweryBeers = await connection.QueryAsync<BreweryBeerDTO>(query);
                return breweryBeers;
            }
        }

        public async Task<IEnumerable<BreweryBeerDTO>> GetByBreweryId(int breweryId)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryBeerRepository)}");
            var query = @"SELECT bb.Id as Id, b.Id as BreweryId, b.Name as BreweryName, be.Id as BeerId, be.Name as BeerName, be.PercentageAlcoholByVolume as PercentageAlcoholByVolume
                            FROM Brewery b 
                            LEFT JOIN BreweryBeer bb ON b.Id = bb.BreweryId
                            LEFT JOIN Beer be on be.Id = bb.BeerId
                            WHERE b.Id = @BreweryId";

            var parameters = new DynamicParameters();
            parameters.Add("BreweryId", breweryId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var breweryBeers = await connection.QueryAsync<BreweryBeerDTO>(query, parameters);
                return breweryBeers;
            }
        }
    }
}

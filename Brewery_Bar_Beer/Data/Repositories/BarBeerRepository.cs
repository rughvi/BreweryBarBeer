using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public class BarBeerRepository : IBarBeerRepository
    {
        private readonly ILogger<BarBeerRepository> _logger;
        private readonly DapperContext _context;

        public BarBeerRepository(ILogger<BarBeerRepository> logger, DapperContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Create(BarBeerDTO barBeerDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BarBeerRepository)}");
            var query = @"IF EXISTS (SELECT * FROM Bar WHERE Id = @BarId ) 
                            AND EXISTS (SELECT * FROM Beer WHERE Id = @BeerId)
                            BEGIN
                                INSERT INTO BarBeer (BarId, BeerId) VALUES (@BarId, @BeerId)
                            END";
            var parameters = new DynamicParameters();
            parameters.Add("BarId", barBeerDTO.BarId, DbType.Int32);
            parameters.Add("BeerId", barBeerDTO.BeerId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var result = await connection.ExecuteAsync(query, parameters);
                    if(result == -1)
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

        public async Task<IEnumerable<BarBeerDTO>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BarBeerRepository)}");
            var query = @"SELECT bb.Id as Id, b.Id as BarId, b.Name as BarName, b.Address as BarAddress, be.Id as BeerId, be.Name as BeerName, be.PercentageAlcoholByVolume as PercentageAlcoholByVolume
                            FROM Bar b 
                            LEFT JOIN BarBeer bb ON b.Id = bb.barId
                            LEFT JOIN Beer be on be.Id = bb.BeerId";

            using (var connection = _context.CreateConnection())
            {
                var barBeers = await connection.QueryAsync<BarBeerDTO>(query);
                return barBeers;
            }
        }

        public async Task<IEnumerable<BarBeerDTO>> GetByBarId(int barId)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BarBeerRepository)}");
            var query = @"SELECT bb.Id as Id, b.Id as BarId, b.Name as BarName, b.Address as BarAddress, be.Id as BeerId, be.Name as BeerName, be.PercentageAlcoholByVolume as PercentageAlcoholByVolume
                            FROM Bar b 
                            LEFT JOIN BarBeer bb ON b.Id = bb.barId
                            LEFT JOIN Beer be on be.Id = bb.BeerId
                            WHERE b.Id = @BarId";
            var parameters = new DynamicParameters();
            parameters.Add("BarId", barId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var barBeers = await connection.QueryAsync<BarBeerDTO>(query, parameters);
                return barBeers;
            }
        }
    }
}

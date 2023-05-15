using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ILogger<BeerRepository> _logger;
        private readonly DapperContext _context;
        public BeerRepository(ILogger<BeerRepository> logger, DapperContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Create(BeerDTO beerDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BeerRepository)}");
            var query = "INSERT INTO Beer (Name, PercentageAlcoholByVolume) VALUES (@Name, @PercentageAlcoholByVolume)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", beerDTO.Name, DbType.String);
            parameters.Add("PercentageAlcoholByVolume", beerDTO.PercentageAlcoholByVolume, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<BeerDTO>> GetAll(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BeerRepository)}");
            var query = @"SELECT * FROM Beer WHERE
                            (@gtAlcoholByVolume IS NULL OR PercentageAlcoholByVolume >= @gtAlcoholByVolume) AND
                            (@ltAlcoholByVolume IS NULL OR PercentageAlcoholByVolume <= @ltAlcoholByVolume)";

            var parameters = new DynamicParameters();
            parameters.Add("gtAlcoholByVolume", gtAlcoholByVolume, DbType.Decimal);
            parameters.Add("ltAlcoholByVolume", ltAlcoholByVolume, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                var beerDTOs = await connection.QueryAsync<BeerDTO>(query, parameters);
                return beerDTOs;
            }
        }

        public async Task<BeerDTO> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BeerRepository)}");
            var query = "SELECT * FROM Beer WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var beer = await connection.QuerySingleOrDefaultAsync<BeerDTO>(query, parameters);
                return beer;
            }
        }

        public async Task Update(BeerDTO beerDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BeerRepository)}");
            var query = "UPDATE Beer SET Name = @Name, PercentageAlcoholByVolume=@PercentageAlcoholByVolume WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", beerDTO.Id, DbType.Int32);
            parameters.Add("Name", beerDTO.Name, DbType.String);
            parameters.Add("PercentageAlcoholByVolume", beerDTO.PercentageAlcoholByVolume, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}

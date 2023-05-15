using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly ILogger<BreweryRepository> _logger;
        private readonly DapperContext _context;
        public BreweryRepository(ILogger<BreweryRepository> logger, DapperContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Create(BreweryDTO breweryDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryRepository)}");
            var query = "INSERT INTO Brewery (Name) VALUES (@Name)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", breweryDTO.Name, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }            
        }

        public async Task<IEnumerable<BreweryDTO>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BreweryRepository)}");
            var query = "SELECT * FROM Brewery";
            using (var connection = _context.CreateConnection())
            {
                var breweries = await connection.QueryAsync<BreweryDTO>(query);
                return breweries.ToList();
            }
        }

        public async Task<BreweryDTO> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BreweryRepository)}");
            var query = "SELECT * FROM Brewery WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var brewery = await connection.QuerySingleOrDefaultAsync<BreweryDTO>(query, parameters);
                return brewery;
            }
        }

        public async Task Update(BreweryDTO breweryDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BreweryRepository)}");
            var query = "UPDATE Brewery SET Name = @Name WHERE Id = @Id";
            //var query = @"IF EXISTS (SELECT 1 FROM Brewery WHERE Id = @Id)
            //                BEGIN
            //                    UPDATE Brewery SET Name=@Name WHERE Id = @Id
            //                END
            //                ELSE
            //                BEGIN
            //                    RAISERROR('Brewery does not exists to update', 10, 1);
            //                    RETURN;
            //                END";
            var parameters = new DynamicParameters();
            parameters.Add("Id", breweryDTO.Id, DbType.Int32);
            parameters.Add("Name", breweryDTO.Name, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                }
                catch (Exception ex)
                {
                    throw;
                }                
            }
        }
    }
}

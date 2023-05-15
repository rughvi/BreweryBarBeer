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
    public class BarRepository : IBarRepository
    {
        private readonly ILogger<BarRepository> _logger;
        private readonly DapperContext _context;

        public BarRepository(ILogger<BarRepository> logger, DapperContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Create(BarDTO barDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BarRepository)}");
            var query = "INSERT INTO Bar (Name, Address) VALUES (@Name, @Address)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", barDTO.Name, DbType.String);
            parameters.Add("Address", barDTO.Address, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<BarDTO>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BarRepository)}");
            var query = "SELECT * FROM Bar";
            using (var connection = _context.CreateConnection())
            {
                var barDTOs = await connection.QueryAsync<BarDTO>(query);
                return barDTOs.ToList();
            }
        }

        public async Task<BarDTO> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BarRepository)}");
            var query = "SELECT * FROM Bar WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var barDTO = await connection.QuerySingleOrDefaultAsync<BarDTO>(query, parameters);
                return barDTO;
            }
        }

        public async Task Update(BarDTO barDTO)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BarRepository)}");
            var query = "UPDATE Bar SET Name = @Name, Address=@Address WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", barDTO.Id, DbType.Int32);
            parameters.Add("Name", barDTO.Name, DbType.String);
            parameters.Add("Address", barDTO.Address, DbType.String);

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

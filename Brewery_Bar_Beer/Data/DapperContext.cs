using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Brewery_Bar_Beer.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("DapperConnection"));

        /// <summary>
        /// This method will setup the tables we require. This is not to be used in production.
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            // create database tables if they don't exist
            using var connection = CreateConnection();
            await _initBreweries();
            await _initBeers();
            await _initBars();
            await _initBarBeers();
            await _initBreweryBeers();

            async Task _initBreweries()
            {
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Brewery' and xtype='U')
                                CREATE TABLE Brewery (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    Name nvarchar(255) NOT NULL
                                )";
                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            async Task _initBeers()
            {
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Beer' and xtype='U')
                                CREATE TABLE Beer (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    Name nvarchar(255) NOT NULL,
                                    PercentageAlcoholByVolume DECIMAL(4,2) NOT NULL
                                )";
                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            async Task _initBars()
            {
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Bar' and xtype='U')
                                CREATE TABLE Bar (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    Name nvarchar(255) NOT NULL,
                                    Address nvarchar(255) NOT NULL
                                )";
                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            async Task _initBarBeers()
            {
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BarBeer' and xtype='U')
                                CREATE TABLE BarBeer(
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    BarId INT NOT NULL,
                                    BeerId INT NOT NULL,
                                    FOREIGN KEY (BarId) REFERENCES Bar(Id),
                                    FOREIGN KEY (BeerId) REFERENCES Beer(Id),
                                    UNIQUE(BarId, BeerId)
                                )";
                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            async Task _initBreweryBeers()
            {
                var sql = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BreweryBeer' and xtype='U')
                                CREATE TABLE BreweryBeer(
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    BreweryId INT NOT NULL,
                                    BeerId INT NOT NULL,
                                    FOREIGN KEY (BreweryId) REFERENCES Brewery(Id),
                                    FOREIGN KEY (BeerId) REFERENCES Beer(Id),
                                    UNIQUE(BreweryId, BeerId)
                                )";
                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}

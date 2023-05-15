using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;

namespace Brewery_Bar_Beer.Services
{
    public interface IBreweryService
    {
        Task<IEnumerable<BreweryResponse>> GetAll();

        Task<BreweryResponse> GetById(int id);

        Task Create(CreateBreweryRequest brewery);

        Task Update(int id, UpdateBreweryRequest brewery);
    }
}

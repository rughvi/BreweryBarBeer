using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;

namespace Brewery_Bar_Beer.Services
{
    public interface IBreweryBeerService
    {
        Task<bool> Create(CreateBreweryBeerRequest createBreweryBeerRequest);
        Task<IEnumerable<BreweryBeerResponse>> GetAll();
        Task<BreweryBeerResponse> GetBreweryByIdWithBeers(int breweryId);
    }
}

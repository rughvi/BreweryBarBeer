using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;

namespace Brewery_Bar_Beer.Services
{
    public interface IBarBeerService
    {
        Task<bool> Create(CreateBarBeerRequest createBarBeerRequest);
        Task<IEnumerable<BarBeerResponse>> GetAll();
        Task<BarBeerResponse> GetBarByIdWithBeers(int barId);
    }
}

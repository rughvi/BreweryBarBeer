using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;

namespace Brewery_Bar_Beer.Services
{
    public interface IBeerService
    {
        Task<IEnumerable<BeerResponse>> GetAll(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume);

        Task<BeerResponse> GetById(int id);

        Task Create(CreateBeerRequest beer);

        Task Update(int id, UpdateBeerRequest beer);
    }
}

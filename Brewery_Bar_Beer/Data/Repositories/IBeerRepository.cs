using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public interface IBeerRepository
    {
        Task Create(BeerDTO beerDTO);
        Task Update(BeerDTO beerDTO);
        Task<BeerDTO> GetById(int id);
        Task<IEnumerable<BeerDTO>> GetAll(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume);
    }
}

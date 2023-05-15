using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public interface IBreweryBeerRepository
    {
        Task<bool> Create(BreweryBeerDTO barDTO);
        Task<IEnumerable<BreweryBeerDTO>> GetAll();
        Task<IEnumerable<BreweryBeerDTO>> GetByBreweryId(int breweryId);
    }
}

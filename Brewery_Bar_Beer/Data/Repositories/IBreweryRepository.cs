using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public interface IBreweryRepository
    {
        Task<IEnumerable<BreweryDTO>> GetAll();

        Task<BreweryDTO> GetById(int id);

        Task Create(BreweryDTO breweryDTO);

        Task Update(BreweryDTO breweryDTO);
    }
}

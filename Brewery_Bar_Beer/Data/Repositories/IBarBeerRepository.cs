using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public interface IBarBeerRepository
    {
        Task<bool> Create(BarBeerDTO barDTO);
        Task<IEnumerable<BarBeerDTO>> GetAll();
        Task<IEnumerable<BarBeerDTO>> GetByBarId(int barId);
    }
}

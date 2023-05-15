using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Data.DTOs;

namespace Brewery_Bar_Beer.Data.Repositories
{
    public interface IBarRepository
    {
        Task<IEnumerable<BarDTO>> GetAll();

        Task<BarDTO> GetById(int id);

        Task Create(BarDTO barDTO);

        Task Update(BarDTO barDTO);
    }
}

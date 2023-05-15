using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;

namespace Brewery_Bar_Beer.Services
{
    public interface IBarService
    {
        Task<IEnumerable<BarResponse>> GetAll();

        Task<BarResponse> GetById(int id);

        Task Create(CreateBarRequest brewery);

        Task Update(int id, UpdateBarRequest brewery);
    }
}

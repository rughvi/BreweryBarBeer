using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Brewery_Bar_Beer.Data.DTOs;
using Brewery_Bar_Beer.Data.Repositories;
using Brewery_Bar_Beer.Models;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Services
{
    public class BarService : IBarService
    {
        private readonly ILogger<BarService> _logger;
        private readonly IMapper _mapper;
        private readonly IBarRepository _barRepository;

        public BarService(ILogger<BarService> logger, IMapper mapper, IBarRepository barRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _barRepository = barRepository;
        }

        public async Task Create(CreateBarRequest bar)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BarService)}");
            var barDTO = _mapper.Map<BarDTO>(bar);
            await _barRepository.Create(barDTO);
        }

        public async Task<IEnumerable<BarResponse>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BarService)}");

            var barDTOs = await _barRepository.GetAll();
            var bars = _mapper.Map<IEnumerable<BarResponse>>(barDTOs);

            return bars;
        }

        public async Task<BarResponse> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BarService)}");
            var barDTO = await _barRepository.GetById(id);

            var bar = _mapper.Map<BarResponse>(barDTO);
            return bar;
        }

        public async Task Update(int id, UpdateBarRequest bar)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BarService)}");
            var barDTO = _mapper.Map<BarDTO>(bar);
            barDTO.Id = id;
            // Should we check if a bar of given id exists?
            // If does not exist, should we send the client any error status code?
            await _barRepository.Update(barDTO);
        }
    }
}

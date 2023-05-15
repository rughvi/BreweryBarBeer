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
    public class BreweryService : IBreweryService
    {
        private readonly ILogger<BreweryService> _logger;
        private readonly IMapper _mapper;
        private readonly IBreweryRepository _breweryRepository;

        public BreweryService(ILogger<BreweryService> logger, IMapper mapper, IBreweryRepository breweryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _breweryRepository = breweryRepository;
        }

        public async Task Create(CreateBreweryRequest brewery)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryService)}");
            var breweryDTO = _mapper.Map<BreweryDTO>(brewery);
            await _breweryRepository.Create(breweryDTO);
        }

        public async Task<IEnumerable<BreweryResponse>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BreweryService)}");

            var breweryDTOs = await _breweryRepository.GetAll();
            var breweries = _mapper.Map<IEnumerable<BreweryResponse>>(breweryDTOs);

            return breweries;
        }

        public async Task<BreweryResponse> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BreweryService)}");
            var breweryDTO = await _breweryRepository.GetById(id);

            var brewery = _mapper.Map<BreweryResponse>(breweryDTO);
            return brewery;
        }

        public async Task Update(int id, UpdateBreweryRequest brewery)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BreweryService)}");
            var breweryDTO = _mapper.Map<BreweryDTO>(brewery);
            breweryDTO.Id = id;
            // Should we check if a brewery of given id exists?
            // If does not exist, should we send the client any error status code?
            await _breweryRepository.Update(breweryDTO);
        }
    }
}

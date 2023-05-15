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
    public class BeerService : IBeerService
    {
        private readonly ILogger<BeerService> _logger;
        private readonly IMapper _mapper;
        private readonly IBeerRepository _beerRepository;

        public BeerService(ILogger<BeerService> logger, IMapper mapper, IBeerRepository beerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _beerRepository = beerRepository;
        }

        public async Task Create(CreateBeerRequest beer)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BeerService)}");
            var beerDTO = _mapper.Map<BeerDTO>(beer);
            await _beerRepository.Create(beerDTO);
        }

        public async Task<IEnumerable<BeerResponse>> GetAll(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BeerService)}");
            var beerDTOs = await _beerRepository.GetAll(gtAlcoholByVolume, ltAlcoholByVolume);

            var beers = _mapper.Map<IEnumerable<BeerResponse>>(beerDTOs);
            return beers;
        }

        public async Task<BeerResponse> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BeerService)}");
            var beerDTO = await _beerRepository.GetById(id);

            var beer = _mapper.Map<BeerResponse>(beerDTO);
            return beer;
        }

        public async Task Update(int id, UpdateBeerRequest beer)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BeerService)}");
            var beerDTO = _mapper.Map<BeerDTO>(beer);
            beerDTO.Id = id;
            // Should we check if a beer of given id exists?
            // If does not exist, should we send the client any error status code?
            await _beerRepository.Update(beerDTO);
        }
    }
}

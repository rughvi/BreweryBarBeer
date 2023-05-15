using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Brewery_Bar_Beer.Data.DTOs;
using Brewery_Bar_Beer.Data.Repositories;
using Brewery_Bar_Beer.Models;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Services
{
    public class BreweryBeerService : IBreweryBeerService
    {
        private readonly ILogger<BreweryBeerService> _logger;
        private readonly IMapper _mapper;
        private readonly IBreweryBeerRepository _breweryBeerRepository;

        public BreweryBeerService(ILogger<BreweryBeerService> logger, IMapper mapper, IBreweryBeerRepository breweryBeerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _breweryBeerRepository = breweryBeerRepository;
        }

        public async Task<bool> Create(CreateBreweryBeerRequest createBreweryBeerRequest)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryBeerService)}");
            var breweryBeerDTO = _mapper.Map<BreweryBeerDTO>(createBreweryBeerRequest);
            return await _breweryBeerRepository.Create(breweryBeerDTO);
        }

        public async Task<IEnumerable<BreweryBeerResponse>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BreweryBeerResponse)}");
            var breweryBeerDTOs = await _breweryBeerRepository.GetAll();

            var grouped = breweryBeerDTOs.GroupBy(b => b.BreweryId);
            var breweryBeerReponses = new List<BreweryBeerResponse>();

            foreach (var group in grouped)
            {
                var breweryBeerReponse = new BreweryBeerResponse()
                {
                    BreweryId = group.First().BreweryId,
                    BreweryName = group.First().BreweryName,
                    Beers = new List<BeerResponse>()
                };

                foreach (var value in group)
                {
                    if (value.BeerId.HasValue)
                    {
                        breweryBeerReponse.Beers.Add(new BeerResponse
                        {
                            Id = value.BeerId.Value,
                            Name = value.BeerName,
                            PercentageAlcoholByVolume = value.PercentageAlcoholByVolume.Value
                        });
                    }
                }

                breweryBeerReponses.Add(breweryBeerReponse);

            }
            return breweryBeerReponses;
        }

        public async Task<BreweryBeerResponse> GetBreweryByIdWithBeers(int breweryId)
        {
            _logger.LogInformation($"Calling method {nameof(GetBreweryByIdWithBeers)} of {nameof(BreweryBeerService)}");
            var breweryBeerDTOs = await _breweryBeerRepository.GetByBreweryId(breweryId);
            if (breweryBeerDTOs == null || breweryBeerDTOs.Count() == 0)
            {
                return null;
            }

            var grouped = breweryBeerDTOs.GroupBy(b => b.BreweryId);
            var breweryBeerResponse = new BreweryBeerResponse()
            {
                BreweryId = breweryBeerDTOs.First().BreweryId,
                BreweryName = breweryBeerDTOs.First().BreweryName,
                Beers = new List<BeerResponse>()
            };

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    if (value.BeerId.HasValue)
                    {
                        breweryBeerResponse.Beers.Add(new BeerResponse
                        {
                            Id = value.BeerId.Value,
                            Name = value.BeerName,
                            PercentageAlcoholByVolume = value.PercentageAlcoholByVolume.Value
                        });
                    }
                }

            }
            return breweryBeerResponse;
        }
    }
}

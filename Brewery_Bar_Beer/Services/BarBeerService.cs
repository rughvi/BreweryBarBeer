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
    public class BarBeerService : IBarBeerService
    {
        private readonly ILogger<BarBeerService> _logger;
        private readonly IMapper _mapper;
        private readonly IBarBeerRepository _barBeerRepository;

        public BarBeerService(ILogger<BarBeerService> logger, IMapper mapper, IBarBeerRepository barBeerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _barBeerRepository = barBeerRepository;
        }

        public async Task<bool> Create(CreateBarBeerRequest createBarBeerRequest)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BarBeerService)}");
            var barDTO = _mapper.Map<BarBeerDTO>(createBarBeerRequest);
            return await _barBeerRepository.Create(barDTO);
        }

        public async Task<IEnumerable<BarBeerResponse>> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BarBeerService)}");
            var barBeerDTOs = await _barBeerRepository.GetAll();
            //if (barBeerDTOs == null || barBeerDTOs.Count() == 0)
            //{
            //    return null;
            //}

            var grouped = barBeerDTOs.GroupBy(b => b.BarId);
            var barBeerResponses = new List<BarBeerResponse>();

            foreach (var group in grouped)
            {
                var barBeerResponse = new BarBeerResponse()
                {
                    BarId = group.First().BarId,
                    BarName = group.First().BarName,
                    Beers = new List<BeerResponse>()
                };

                foreach (var value in group)
                {
                    if(value.BeerId.HasValue)
                    {
                        barBeerResponse.Beers.Add(new BeerResponse
                        {
                            Id = value.BeerId.Value,
                            Name = value.BeerName,
                            PercentageAlcoholByVolume = value.PercentageAlcoholByVolume.Value
                        });
                    }                    
                }

                barBeerResponses.Add(barBeerResponse);

            }
            return barBeerResponses;
        }

        public async Task<BarBeerResponse> GetBarByIdWithBeers(int barId)
        {
            _logger.LogInformation($"Calling method {nameof(GetBarByIdWithBeers)} of {nameof(BarBeerService)}");
            var barBeerDTOs = await _barBeerRepository.GetByBarId(barId);
            if(barBeerDTOs == null || barBeerDTOs.Count() == 0)
            {
                return null;
            }

            var grouped = barBeerDTOs.GroupBy(b => b.BarId);
            var barBeerResponse = new BarBeerResponse()
            {
                BarId = barBeerDTOs.First().BarId,
                BarName = barBeerDTOs.First().BarName,
                Beers = new List<BeerResponse>()
            };

            foreach(var group in grouped)
            {
                foreach(var value in group)
                {
                    if(value.BeerId.HasValue)
                    {
                        barBeerResponse.Beers.Add(new BeerResponse
                        {
                            Id = value.BeerId.Value,
                            Name = value.BeerName,
                            PercentageAlcoholByVolume = value.PercentageAlcoholByVolume.Value
                        });
                    }                    
                }
                
            }
            return barBeerResponse;
        }
    }
}

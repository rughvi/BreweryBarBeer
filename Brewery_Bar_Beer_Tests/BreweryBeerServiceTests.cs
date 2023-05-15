using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Brewery_Bar_Beer.Data.DTOs;
using Brewery_Bar_Beer.Data.Repositories;
using Brewery_Bar_Beer.Mappers;
using Brewery_Bar_Beer.Models;
using Brewery_Bar_Beer.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Brewery_Bar_Beer_Tests
{
    public class BreweryBeerServiceTests
    {
        private Mock<IBreweryBeerRepository> _breweryBeerRepositoryMock;
        private Mock<ILogger<BreweryBeerService>> _loggerMock;
        private IMapper _mapper;
        private BreweryBeerService _breweryBeerService;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<BreweryBeerService>>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _breweryBeerRepositoryMock = new Mock<IBreweryBeerRepository>();
            _breweryBeerService = new BreweryBeerService(_loggerMock.Object, _mapper, _breweryBeerRepositoryMock.Object);
        }

        [Test]
        public async Task Test_GetAll()
        {
            _breweryBeerRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<BreweryBeerDTO>()
            {
                new BreweryBeerDTO{ Id = 1, BreweryId=1, BreweryName = "Brewery 1", BeerId = 1, BeerName = "Beer 1", PercentageAlcoholByVolume=4.11M },
                new BreweryBeerDTO{ Id = 2, BreweryId=1, BreweryName = "Brewery 1", BeerId = 2, BeerName = "Beer 2", PercentageAlcoholByVolume=4.45M },
                new BreweryBeerDTO{ Id = 3, BreweryId=1, BreweryName = "Brewery 1", BeerId = 3, BeerName = "Beer 3", PercentageAlcoholByVolume=5.78M },
                new BreweryBeerDTO{ Id = 4, BreweryId=2, BreweryName = "Brewery 2", BeerId = 2, BeerName = "Beer 2", PercentageAlcoholByVolume=4.45M },
                new BreweryBeerDTO{ Id = 5, BreweryId=3, BreweryName = "Brewery 3" },
            });

            IEnumerable<BreweryBeerResponse> breweryBeers = await _breweryBeerService.GetAll();

            Assert.AreEqual(3, breweryBeers.Count());
            Assert.AreEqual(3, breweryBeers.Single(b => b.BreweryName == "Brewery 1").Beers.Count);
            Assert.AreEqual(1, breweryBeers.Single(b => b.BreweryName == "Brewery 2").Beers.Count);
            Assert.AreEqual(0, breweryBeers.Single(b => b.BreweryName == "Brewery 3").Beers.Count);
            _breweryBeerRepositoryMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public async Task Test_GetBreweryByIdWithBeers()
        {
            _breweryBeerRepositoryMock.Setup(r => r.GetByBreweryId(It.IsAny<int>())).ReturnsAsync(new List<BreweryBeerDTO>()
            {
                new BreweryBeerDTO{ Id = 1, BreweryId=1, BreweryName = "Brewery 1", BeerId = 1, BeerName = "Beer 1", PercentageAlcoholByVolume=4.11M },
                new BreweryBeerDTO{ Id = 2, BreweryId=1, BreweryName = "Brewery 1", BeerId = 2, BeerName = "Beer 2", PercentageAlcoholByVolume=4.45M },
                new BreweryBeerDTO{ Id = 3, BreweryId=1, BreweryName = "Brewery 1", BeerId = 3, BeerName = "Beer 3", PercentageAlcoholByVolume=5.78M }
            });

            BreweryBeerResponse breweryBeers = await _breweryBeerService.GetBreweryByIdWithBeers(1);

            Assert.NotNull(breweryBeers);
            Assert.AreEqual(3, breweryBeers.Beers.Count);
            _breweryBeerRepositoryMock.Verify(r => r.GetByBreweryId(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task Test_Create()
        {
            _breweryBeerRepositoryMock.Setup(r => r.Create(It.IsAny<BreweryBeerDTO>())).ReturnsAsync(true);
            var result = await _breweryBeerService.Create(new CreateBreweryBeerRequest());

            Assert.True(result);
            _breweryBeerRepositoryMock.Verify(r => r.Create(It.IsAny<BreweryBeerDTO>()), Times.Once);
        }
    }
}

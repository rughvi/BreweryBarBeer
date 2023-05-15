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
    public class BreweryServiceTests
    {

        private Mock<IBreweryRepository> _breweryRepositoryMock;
        private Mock<ILogger<BreweryService>> _loggerMock;
        private IMapper _mapper;
        private BreweryService _breweryService;
        [SetUp]
        public void Setup()
        {
            try
            {
                _loggerMock = new Mock<ILogger<BreweryService>>();

                if (_mapper == null)
                {
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MapperProfile());
                    });
                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }

                _breweryRepositoryMock = new Mock<IBreweryRepository>();
                _breweryService = new BreweryService(_loggerMock.Object, _mapper, _breweryRepositoryMock.Object);
            }
            catch (System.Exception ex)
            {
                throw;
            }            
        }

        [Test]
        public async Task Test_GetAll()
        {
            _breweryRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<BreweryDTO>()
            {
                new BreweryDTO{ Id = 1, Name = "Brewery 1" },
                new BreweryDTO{ Id = 2, Name = "Brewery 2" }
            });

            IEnumerable<BreweryResponse> breweries = await _breweryService.GetAll();
            
            Assert.AreEqual(2, breweries.Count());
            _breweryRepositoryMock.Verify(r => r.GetAll(), Times.Once);
        }
    }
}

using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;
using Brewery_Bar_Beer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreweryController : Controller
    {
        private readonly ILogger<BreweryController> _logger;
        private readonly IBreweryService _breweryService;
        private readonly IBreweryBeerService _breweryBeerService;
        public BreweryController(ILogger<BreweryController> logger, IBreweryService breweryService, IBreweryBeerService breweryBeerService)
        {
            _logger = logger;
            _breweryService = breweryService;
            _breweryBeerService = breweryBeerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BreweryController)}");
            var breweries = await _breweryService.GetAll();
            return Ok(breweries);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BreweryController)}");
            var brewery = await _breweryService.GetById(id);
            if(brewery == null)
            {
                return NotFound($"Brewery not found for id {id}");
            }
            
            return Ok(brewery);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBreweryRequest brewery)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BreweryController)}");
            await _breweryService.Create(brewery);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBreweryRequest brewery)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BreweryController)}");
            await _breweryService.Update(id, brewery);

            return Ok();
        }

        [HttpPost("beer")]
        public async Task<IActionResult> CreateBreweryWithBeer([FromBody] CreateBreweryBeerRequest createBreweryBeerRequest)
        {
            _logger.LogInformation($"Calling method {nameof(CreateBreweryWithBeer)} of {nameof(BreweryController)}");
            var result = await _breweryBeerService.Create(createBreweryBeerRequest);
            if (!result)
            {
                return NotFound("Invalid breweryId or beerId");
            }
            return Ok();
        }

        [HttpGet("{breweryId}/beer")]
        public async Task<IActionResult> GetBreweryByIdWithBeers(int breweryId)
        {
            _logger.LogInformation($"Calling method {nameof(GetBreweryByIdWithBeers)} of {nameof(BreweryController)}");
            var result = await _breweryBeerService.GetBreweryByIdWithBeers(breweryId);
            if (result == null)
            {
                return NotFound($"Brewery not found for id {breweryId}");
            }
            return Ok(result);
        }

        [HttpGet("beer")]
        public async Task<IActionResult> GetBreweriesWithBeers()
        {
            _logger.LogInformation($"Calling method {nameof(GetBreweriesWithBeers)} of {nameof(BreweryController)}");
            var result = await _breweryBeerService.GetAll();

            return Ok(result);
        }
    }
}

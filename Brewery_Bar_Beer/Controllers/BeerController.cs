using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;
using Brewery_Bar_Beer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Brewery_Bar_Beer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeerController : Controller
    {
        private readonly ILogger<BeerController> _logger;
        private readonly IBeerService _beerService;

        public BeerController(ILogger<BeerController> logger, IBeerService beerService)
        {
            _logger = logger;
            _beerService = beerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBeerRequest beer)
        {
            _logger.LogInformation($"Calling method {nameof(Create)} of {nameof(BeerController)}");
            await _beerService.Create(beer);
            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery]decimal? gtAlcoholByVolume, [FromQuery]decimal? ltAlcoholByVolume)
        {
            _logger.LogInformation($"Calling method {nameof(GetAll)} of {nameof(BeerController)}");
            var beers = await _beerService.GetAll(gtAlcoholByVolume, ltAlcoholByVolume);
            return Ok(beers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetById)} of {nameof(BeerController)}");
            var beer = await _beerService.GetById(id);
            if (beer == null)
            {
                return NotFound($"Beer not found for id {id}");
            }

            return Ok(beer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBeerRequest beer)
        {
            _logger.LogInformation($"Calling method {nameof(Update)} of {nameof(BeerController)}");
            await _beerService.Update(id, beer);

            return Ok();
        }
    }
}

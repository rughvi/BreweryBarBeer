using System;
using System.Threading.Tasks;
using Brewery_Bar_Beer.Models;
using Brewery_Bar_Beer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Brewery_Bar_Beer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarController : Controller
    {
        private readonly ILogger<BarController> _logger;
        private readonly IBarService _barService;
        private readonly IBarBeerService _barBeerService;
        public BarController(ILogger<BarController> logger, IBarService barService, IBarBeerService barBeerService)
        {
            _logger = logger;
            _barService = barService;
            _barBeerService = barBeerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBars()
        {
            _logger.LogInformation($"Calling method {nameof(GetAllBars)} of {nameof(BarController)}");
            var bars = await _barService.GetAll();
            return Ok(bars);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBarById(int id)
        {
            _logger.LogInformation($"Calling method {nameof(GetBarById)} of {nameof(BarController)}");
            var bar = await _barService.GetById(id);
            if (bar == null)
            {
                return NotFound($"Bar not found for id {id}");
            }

            return Ok(bar);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBar([FromBody] CreateBarRequest bar)
        {
            _logger.LogInformation($"Calling method {nameof(CreateBar)} of {nameof(BarController)}");
            await _barService.Create(bar);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBar(int id, [FromBody] UpdateBarRequest bar)
        {
            _logger.LogInformation($"Calling method {nameof(UpdateBar)} of {nameof(BarController)}");
            await _barService.Update(id, bar);

            return Ok();
        }

        [HttpPost("beer")]
        public async Task<IActionResult> CreateBarWithBeer([FromBody] CreateBarBeerRequest createBarBeerRequest)
        {
            _logger.LogInformation($"Calling method {nameof(CreateBarWithBeer)} of {nameof(BarController)}");
            var result = await _barBeerService.Create(createBarBeerRequest);
            if(!result)
            {
                return NotFound("Invalid barId or beerId");
            }
            return Ok();
        }

        [HttpGet("{barId}/beer")]
        public async Task<IActionResult> GetBarByIdWithBeers(int barId)
        {
            _logger.LogInformation($"Calling method {nameof(GetBarByIdWithBeers)} of {nameof(BarController)}");
            var result = await _barBeerService.GetBarByIdWithBeers(barId);
            if(result == null)
            {
                return NotFound($"Bar not found for id {barId}");
            }
            return Ok(result);
        }

        [HttpGet("beer")]
        public async Task<IActionResult> GetBarsWithBeers()
        {
            _logger.LogInformation($"Calling method {nameof(GetBarsWithBeers)} of {nameof(BarController)}");
            var result = await _barBeerService.GetAll();
            
            return Ok(result);
        }
    }
}

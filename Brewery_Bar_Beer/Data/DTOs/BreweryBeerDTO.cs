using System;
namespace Brewery_Bar_Beer.Data.DTOs
{
    public class BreweryBeerDTO
    {
        public int? Id { get; set; }
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public int? BeerId { get; set; }
        public string? BeerName { get; set; }
        public decimal? PercentageAlcoholByVolume { get; set; }
    }
}

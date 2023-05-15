using System;
namespace Brewery_Bar_Beer.Data.DTOs
{
    public class BarBeerDTO
    {
        public int? Id { get; set; }
        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarAddress { get; set; }
        public int? BeerId { get; set; }
        public string? BeerName { get; set; }
        public decimal? PercentageAlcoholByVolume { get; set; }
    }
}

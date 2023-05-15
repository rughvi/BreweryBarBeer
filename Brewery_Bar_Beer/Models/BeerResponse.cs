using System;
namespace Brewery_Bar_Beer.Models
{
    public class BeerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}

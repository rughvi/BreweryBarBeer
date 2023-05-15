using System;
namespace Brewery_Bar_Beer.Models
{
    public class UpdateBeerRequest
    {
        public string Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}

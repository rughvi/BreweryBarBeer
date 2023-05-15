using System;
using System.Collections.Generic;

namespace Brewery_Bar_Beer.Models
{
    public class BreweryBeerResponse
    {
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public List<BeerResponse> Beers { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Brewery_Bar_Beer.Models
{
    public class BarBeerResponse
    {
        public int BarId { get; set; }
        public string BarName { get; set; }
        public List<BeerResponse> Beers { get; set; }
    }
}

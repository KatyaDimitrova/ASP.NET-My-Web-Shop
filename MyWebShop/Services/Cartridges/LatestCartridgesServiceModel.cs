using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebShop.Models.Cartridges
{
    public class LatestCartridgesServiceModel
    {
        public int Id { get; init; }

        public string PrinterBrand { get; init; }

        public string Model { get; init; }
        public string Colour { get; set; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }
    }
}

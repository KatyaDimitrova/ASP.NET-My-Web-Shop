using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebShop.Services.Cartridges
{
    public class CartridgeServiceModel
    {
        public int Id { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }
        public string ColourName { get; init; }
        public string  PrinterBrand { get; init; }
    }
}

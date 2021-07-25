using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebShop.Services.Cartridges
{
    public class CartridgeDetailsServiceModel:CartridgeServiceModel
    {
        public string Description { get; init; }

        public int ColourId { get; init; }

        public int PrinterId { get; init; }
    }
}

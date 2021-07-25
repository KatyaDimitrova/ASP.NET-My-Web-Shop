namespace MyWebShop.Services.Cartridges
{
    using System.Collections.Generic;
    public class CartridgeQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CartridgesPerPage { get; init; }

        public int TotalCartridges { get; init; }

        public IEnumerable<CartridgeServiceModel> Cartridges { get; init; }
    }
}

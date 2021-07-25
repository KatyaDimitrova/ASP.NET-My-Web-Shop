namespace MyWebShop.Models.Cartridges
{
    using MyWebShop.Services.Cartridges;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CartridgesSearchQueryModel
    {
        public const int CartridgesPerPage = 3;
        public string PrinterBrand { get; init; }
        public IEnumerable<CartridgePrinterServiceModel> PrintersBrands { get; set; }
        public IEnumerable<string> Models{ get; set; }
        [Display(Name ="Search by text")]
        public string SearchTerm { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int TotalCartridges { get; set; }
        public CartridgesSorting Sorting { get; init; }
        public IEnumerable<CartridgeServiceModel> Cartridges { get; set; }
    }
}

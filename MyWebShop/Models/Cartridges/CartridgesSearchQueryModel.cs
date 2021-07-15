namespace MyWebShop.Models.Cartridges
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CartridgesSearchQueryModel
    {
        public const int CartridgesPerPage = 3;
        public string PrinterBrand { get; init; }
        public IEnumerable<string> PrintersBrands { get; set; }
        public IEnumerable<string> Model{ get; init; }
        [Display(Name ="Search by text")]
        public string SearchTerm { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int TotalCartridges { get; set; }
        public CartridgesSorting Sorting { get; init; }
        public IEnumerable<AllCartridgesViewModel> Cartridges { get; set; }
    }
}

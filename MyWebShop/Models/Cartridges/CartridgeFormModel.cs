namespace MyWebShop.Models.Cartridges
{
    using MyWebShop.Services.Cartridges;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class CartridgeFormModel
    {
        [Required]
        [StringLength(CartridgeModelMaxLength,MinimumLength=CartridgeModelMinLength)]
        public string Model { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = CartridgeDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }
       
        public decimal Price { get; set; }

        [Display(Name = "Colour")]
        public int ColourId { get; init; }

        public IEnumerable<CartridgeColourServiceModel> Colours { get; set; }
        [Display(Name ="Printer")]
        public int PrinterId { get; init; }
        public IEnumerable<CartridgePrinterServiceModel> Printers { get; set; }
    }
}

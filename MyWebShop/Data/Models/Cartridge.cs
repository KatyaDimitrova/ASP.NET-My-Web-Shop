namespace MyWebShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Cartridge
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(CartridgeModelMaxLength)]
        public string Model { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; init; }
        public int PrinterId { get; set; }
        public Printer Printer { get; init; }
       public IEnumerable<OrderCartridge> OrderCartridges { get; set; } = new List<OrderCartridge>();
    }
}

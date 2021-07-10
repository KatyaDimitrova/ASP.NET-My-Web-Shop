namespace MyWebShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Cartridge
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(CartridgeModelMaxLength)]
        public string Model { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; init; }
    }
}

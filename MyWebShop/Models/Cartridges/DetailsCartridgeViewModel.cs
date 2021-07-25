
namespace MyWebShop.Models.Cartridges
{
    public class DetailsCartridgeViewModel
    {
        public int Id { get; set; }
        public string Model { get; init; }

        public decimal Price { get; init; }

        public string ImageUrl { get; init; }

        public string Description { get; init; }
    }
}

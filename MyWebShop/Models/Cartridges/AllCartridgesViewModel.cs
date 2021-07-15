using MyWebShop.Data.Models;

namespace MyWebShop.Models.Cartridges
{
    public class AllCartridgesViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public string Printer{ get; set; }
       
    }
}

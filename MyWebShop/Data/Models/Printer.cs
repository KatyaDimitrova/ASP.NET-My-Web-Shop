namespace MyWebShop.Data.Models
{
    using System.Collections.Generic;
    public class Printer
    {
        public int Id { get; set; }
        public string Brand { get; set; }
       // public string Model { get; set; }
        public IEnumerable<Cartridge> Cartridges { get; init; } = new List<Cartridge>();
    }
}

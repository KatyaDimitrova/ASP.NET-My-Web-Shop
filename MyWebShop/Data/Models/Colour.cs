namespace MyWebShop.Data.Models
{
    using System.Collections.Generic;
    public class Colour
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public IEnumerable<Cartridge> Cartridges { get; init; } = new List<Cartridge>();
    }
}

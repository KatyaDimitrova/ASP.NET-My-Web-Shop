using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebShop.Data.Models
{
    public class OrderCartridge
    {
        [Required]
        public string OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public string CartridgeId { get; set; }

        public Cartridge Cartridge { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebShop.Data.Models
{
    public class Order
    {

        [Key]
        [Required]
        public int Id { get; init; }

        public DateTime OrderDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<OrderCartridge> OrderCartridges { get; set; } = new List<OrderCartridge>();
    }
}

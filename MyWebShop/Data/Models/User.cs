

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebShop.Data.Models
{
    using static DataConstants;
    public class User:IdentityUser
    {
        [MaxLength(UserFullNameMaxLength)]
        public string FullName { get; set; }
        [MaxLength(UserCityMahLength)]
        public string City { get; set; }
        [MaxLength(UserAddressMaxLenght)]
        public string Address { get; set; }
        [MaxLength(UserPhoneMaxLenght)]
        public string Phone { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}

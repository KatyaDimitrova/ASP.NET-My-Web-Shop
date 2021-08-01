using MyWebShop.Models.Cartridges;
using System.Collections.Generic;


namespace MyWebShop.Models.Home
{
    public class IndexViewModel
    {
        public IList<LatestCartridgesServiceModel> Cartridges { get; init; }
    }
}

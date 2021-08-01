namespace MyWebShop.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MyWebShop.Data;
    using MyWebShop.Models;
    using MyWebShop.Models.Cartridges;
    using MyWebShop.Models.Home;
    using MyWebShop.Services.Cartridges;

    public class HomeController : Controller
    {
        private readonly ICartridgeService cartridges;
        private readonly ApplicationDbContext data;

        public HomeController(ICartridgeService cartridges)
        {
            this.cartridges=cartridges;
        }


        public IActionResult Index()
        {
            var latestCartridges = this.cartridges
                 .Latest()
                 .ToList();



            return View(new IndexViewModel
            {
                Cartridges = latestCartridges
            }) ;
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

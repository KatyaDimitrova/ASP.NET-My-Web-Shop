namespace MyWebShop.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MyWebShop.Data;
    using MyWebShop.Models;
    using MyWebShop.Models.Cartridges;
   

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext data;

        public HomeController(ApplicationDbContext data)
            => this.data = data;


        public IActionResult Index()
        {
            var cartridges = this.data.Cartridges
                .OrderBy(c => c.Id)
                .Select(x => new AllCartridgesViewModel
                {
                    Model = x.Model,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Colour = x.Colour.Name,
                    Price = x.Price,
                    Printer = x.Printer.Brand
                })
                .Take(3)
                .ToList();

            return this.View(cartridges);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

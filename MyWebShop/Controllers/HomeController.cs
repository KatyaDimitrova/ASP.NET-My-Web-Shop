namespace MyWebShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyWebShop.Models;
    using System.Diagnostics;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/Cartridges/All");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

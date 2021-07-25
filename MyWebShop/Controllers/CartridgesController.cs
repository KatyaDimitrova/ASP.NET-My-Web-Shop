namespace MyWebShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebShop.Models.Cartridges;
    using MyWebShop.Services.Cartridges;
    using MyWebShop.Infrastructure;


    public class CartridgesController : Controller
    {
       // private readonly ApplicationDbContext data;
        private readonly ICartridgeService cartridges;

        public CartridgesController(ICartridgeService cartridges)
            => this.cartridges = cartridges;


        public IActionResult Add() => View(new CartridgeFormModel
        {
            Colours = this.cartridges.AllColours(),
            Printers=this.cartridges.AllPrinterBrands()
        });
        



        [HttpPost]
        public IActionResult Add(CartridgeFormModel cartridge)
        {
            if (!this.cartridges.ColourExists(cartridge.ColourId))
            {
                this.ModelState.AddModelError(nameof(cartridge.ColourId), "Colour does not exist.");
            }
            if (!this.cartridges.PrinterBrandExists(cartridge.PrinterId))
            {
                this.ModelState.AddModelError(nameof(cartridge.PrinterId), "Printer Brand does not exist.");
            }

            if (!ModelState.IsValid)
            {
                cartridge.Colours = this.cartridges.AllColours();
                cartridge.Printers = this.cartridges.AllPrinterBrands();

                return View(cartridge);
            }

            this.cartridges.Create(
                cartridge.Model,
                cartridge.Description,
                cartridge.ImageUrl,
                cartridge.Price,
                cartridge.ColourId,
                cartridge.PrinterId);

            return RedirectToAction(nameof(All));

        }


        public IActionResult All([FromQuery]CartridgesSearchQueryModel query)
        {
            var queryResult = this.cartridges.All(
                query.PrinterBrand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                CartridgesSearchQueryModel.CartridgesPerPage);

            var printerBrands = this.cartridges.AllPrinterBrands();

            query.PrintersBrands = printerBrands;
            query.TotalCartridges = queryResult.TotalCartridges;
            query.Cartridges = queryResult.Cartridges;


            return View(query);

        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var cartridge = this.cartridges.Details(id);


            return View(new CartridgeFormModel
            {
                
                Model = cartridge.Model,
                Description = cartridge.Description,
                ImageUrl = cartridge.ImageUrl,
                Price = cartridge.Price,
                ColourId = cartridge.ColourId,
                Colours = this.cartridges.AllColours(),
                PrinterId=cartridge.PrinterId,
                Printers=this.cartridges.AllPrinterBrands()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CartridgeFormModel cartridge)
        {
            

            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!this.cartridges.ColourExists(cartridge.ColourId))
            {
                this.ModelState.AddModelError(nameof(cartridge.ColourId), "Colour does not exist.");
            }

            if (!this.cartridges.PrinterBrandExists(cartridge.PrinterId))
            {
                this.ModelState.AddModelError(nameof(cartridge.PrinterId), "Printer Brand does not exist.");
            }
            if (!ModelState.IsValid)
            {
                cartridge.Colours = this.cartridges.AllColours();
                cartridge.Printers = this.cartridges.AllPrinterBrands();

                return View(cartridges);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.cartridges.Edit(
                id,
                cartridge.Model,
                cartridge.Description,
                cartridge.ImageUrl,
                cartridge.Price,
                cartridge.ColourId,
                cartridge.PrinterId);

            return RedirectToAction(nameof(All));
        }


    }
}

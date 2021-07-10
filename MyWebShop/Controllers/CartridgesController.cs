namespace MyWebShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyWebShop.Data;
    using MyWebShop.Data.Models;
    using MyWebShop.Models.Cartridges;
    using System.Collections.Generic;
    using System.Linq;

    public class CartridgesController:Controller
    {
        private readonly ApplicationDbContext data;

        public CartridgesController(ApplicationDbContext data)
            => this.data = data;


        public IActionResult Add() => View(new AddCartridgeFormModel
        {
            Colours = this.GetCartridgeColours()
        });

        [HttpPost]
        public IActionResult Add(AddCartridgeFormModel cartridge)
        {
            if (!this.data.Colours.Any(c => c.Id == cartridge.ColourId))
            {
                this.ModelState.AddModelError(nameof(cartridge.ColourId), "Colour does not exist.");
            }

            if (!ModelState.IsValid)
            {
                cartridge.Colours = this.GetCartridgeColours();

                return View(cartridge);
            }

            var cartridgeData = new Cartridge
            {
                Model = cartridge.Model,
                Description = cartridge.Description,
                ImageUrl = cartridge.ImageUrl,
                ColourId = cartridge.ColourId
            };

            this.data.Cartridges.Add(cartridgeData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<CartridgeColourViewModel> GetCartridgeColours()
            => this.data
            .Colours
            .Select(c => new CartridgeColourViewModel
            {
                Id=c.Id,
                Name=c.Name
            })
            .ToList();
    }
}

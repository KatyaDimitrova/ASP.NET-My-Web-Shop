namespace MyWebShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyWebShop.Data;
    using MyWebShop.Data.Models;
    using MyWebShop.Models.Cartridges;
    using System.Collections.Generic;
    using System.Linq;

    public class CartridgesController : Controller
    {
        private readonly ApplicationDbContext data;

        public CartridgesController(ApplicationDbContext data)
            => this.data = data;


        public IActionResult Add() => View(new AddCartridgeFormModel
        {
            Colours = this.GetCartridgeColours(),
            Printers = this.GetCartridgePrinters()
        });



        [HttpPost]
        public IActionResult Add(AddCartridgeFormModel cartridge)
        {
            if (!this.data.Colours.Any(c => c.Id == cartridge.ColourId))
            {
                this.ModelState.AddModelError(nameof(cartridge.ColourId), "Colour does not exist.");
            }
            if (!this.data.Printers.Any(p => p.Id == cartridge.PrinterId))
            {
                this.ModelState.AddModelError(nameof(cartridge.PrinterId), "Printer does not exist.");
            }

            if (!ModelState.IsValid)
            {
                cartridge.Colours = this.GetCartridgeColours();
                cartridge.Printers = this.GetCartridgePrinters();

                return View(cartridge);
            }

            var cartridgeData = new Cartridge
            {
                Model = cartridge.Model,
                Description = cartridge.Description,
                ImageUrl = cartridge.ImageUrl,
                ColourId = cartridge.ColourId,
                PrinterId = cartridge.PrinterId
            };

            this.data.Cartridges.Add(cartridgeData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<CartridgeColourViewModel> GetCartridgeColours()
            => this.data
            .Colours
            .Select(c => new CartridgeColourViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        private IEnumerable<CartridgePrinterViewModel> GetCartridgePrinters()
            => this.data
            .Printers
            .Select(p => new CartridgePrinterViewModel
            {
                Id = p.Id,
                Brand = p.Brand,
                // Model = p.Model
            });

        public IActionResult All([FromQuery]CartridgesSearchQueryModel query)
        {
            var cartridgesQuery = this.data.Cartridges.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PrinterBrand))
            {
                cartridgesQuery = cartridgesQuery.Where(c => c.Printer.Brand == query.PrinterBrand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                cartridgesQuery = cartridgesQuery.Where(c =>
                      (c.Printer.Brand+" "+c.Model).ToLower().Contains(query.SearchTerm.ToLower()) ||
                      c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            };

            cartridgesQuery = query.Sorting switch
            {
               CartridgesSorting.DateCreated=>cartridgesQuery.OrderByDescending(c=>c.Id),
               CartridgesSorting.PrinterBrandAndModel=>cartridgesQuery.OrderBy(c=>c.Printer.Brand).ThenBy(c=>c.Model),
               _=>cartridgesQuery.OrderByDescending(c=>c.Id)
            };

            var totalCartridges = cartridgesQuery.Count();

            var cartridges = cartridgesQuery
                .Skip((query.CurrentPage - 1) * CartridgesSearchQueryModel.CartridgesPerPage)
                .Take(CartridgesSearchQueryModel.CartridgesPerPage)
                .Select(x => new AllCartridgesViewModel
            {
                Model = x.Model,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Colour=x.Colour.Name,
                Price=x.Price,
                Printer=x.Printer.Brand
            })
            .ToList();

            var printersBrands = this.data
                .Printers
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br=>br)
                .ToList();

            query.TotalCartridges = totalCartridges;
            query.PrintersBrands = printersBrands;
            query.Cartridges = cartridges;

            return View(query);         

        }
    }
}

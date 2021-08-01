using MyWebShop.Data;
using MyWebShop.Data.Models;
using MyWebShop.Models.Cartridges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebShop.Services.Cartridges
{
    public class CartridgeService:ICartridgeService
    {
        private readonly ApplicationDbContext data;

        public CartridgeService(ApplicationDbContext data)
            => this.data = data;

        public CartridgeQueryServiceModel All(
            string printerBrand,
            string searchTerm,
            CartridgesSorting sorting,
            int currentPage,
            int cartridgesPerPage)
        {
            var cartridgesQuery = this.data.Cartridges.AsQueryable();

            if (!string.IsNullOrWhiteSpace(printerBrand))
            {
                cartridgesQuery = cartridgesQuery.Where(c => c.Printer.Brand == printerBrand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                cartridgesQuery = cartridgesQuery.Where(c =>
                    (c.Printer.Brand + " " + c.Model).ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            cartridgesQuery = sorting switch
            {
                CartridgesSorting.DateCreated => cartridgesQuery.OrderByDescending(c => c.Id),
                CartridgesSorting.PrinterBrandAndModel => cartridgesQuery.OrderBy(c => c.Printer.Brand).ThenBy(c => c.Model),
                _ => cartridgesQuery.OrderByDescending(c => c.Id)
            };

            var totalCartridges = cartridgesQuery.Count();

            var cartridges = GetCartridges(cartridgesQuery
                .Skip((currentPage - 1) * cartridgesPerPage)
                .Take(cartridgesPerPage));


            return new CartridgeQueryServiceModel
            {
                CurrentPage=currentPage,
                CartridgesPerPage=cartridgesPerPage,
                TotalCartridges=totalCartridges,
                Cartridges=cartridges
            };

        }

        public IEnumerable<LatestCartridgesServiceModel> Latest()
            => this.data
                .Cartridges
                .OrderByDescending(c => c.Id)
                .Select(c=>new LatestCartridgesServiceModel
                {
                    Id=c.Id,
                    Model=c.Model,
                    PrinterBrand=c.Printer.Brand,
                    Price=c.Price,
                    Colour=c.Colour.Name,
                    ImageUrl=c.ImageUrl
                })
                .Take(3)
                .ToList();

        public CartridgeDetailsServiceModel Details(int id)
            => this.data
                .Cartridges
                .Where(c => c.Id == id)
                .Select(c => new CartridgeDetailsServiceModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    Price=c.Price,
                    ColourName = c.Colour.Name,
                    PrinterBrand=c.Printer.Brand
                })
                .FirstOrDefault();

        public int Create(string model, string description, string imageUrl, decimal price,int colourId, int printerId)
        {
            var cartridgeData = new Cartridge
            {
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                Price=price,
                ColourId = colourId,
                PrinterId=printerId
            };

            this.data.Cartridges.Add(cartridgeData);
            this.data.SaveChanges();

            return cartridgeData.Id;
        }

        public bool Edit(int id, string model, string description, string imageUrl, decimal price, int colourId,int printerId)
        {
            var cartridgeData = this.data.Cartridges.Find(id);

            if (cartridgeData == null)
            {
                return false;
            }

            cartridgeData.Model = model;
            cartridgeData.Description = description;
            cartridgeData.ImageUrl = imageUrl;
            cartridgeData.Price = price;
            cartridgeData.ColourId = colourId;
            cartridgeData.PrinterId = printerId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<string> AllModels()
              => this.data
                  .Cartridges
                  .Select(c => c.Model)
                  .Distinct()
                  .OrderBy(br => br)
                  .ToList();

        public IEnumerable<CartridgeColourServiceModel> AllColours()
            => this.data
                .Colours
                .Select(c => new CartridgeColourServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public bool ColourExists(int categoryId)
            => this.data
                .Colours
                .Any(c => c.Id == categoryId);

        public IEnumerable<CartridgePrinterServiceModel> AllPrinterBrands()
           => this.data
               .Printers
               .Select(c => new CartridgePrinterServiceModel
               {
                   Id = c.Id,
                   Brand = c.Brand
               })
               .ToList();

        public bool PrinterBrandExists(int printerId)
            => this.data
                .Printers
                .Any(p => p.Id == printerId);


        private static IEnumerable<CartridgeServiceModel> GetCartridges(IQueryable<Cartridge> cartridgeQuery)
            => cartridgeQuery
                .Select(c => new CartridgeServiceModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Price=c.Price,
                    ColourName=c.Colour.Name,
                    PrinterBrand=c.Printer.Brand,
                    
                })
                .ToList();
    }
}


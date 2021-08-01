using MyWebShop.Models.Cartridges;
using System.Collections.Generic;


namespace MyWebShop.Services.Cartridges
{
    public interface ICartridgeService
    {
        CartridgeQueryServiceModel All(
            string printerBrand,
            string searchTerm,
            CartridgesSorting sorting,
            int currentPage,
            int carsPerPage);

        IEnumerable<LatestCartridgesServiceModel> Latest();

        CartridgeDetailsServiceModel Details(int cartridgeId);

        int Create(
            string model,
            string description,
            string imageUrl,
            decimal price,
            int colourId,
            int printerId);

        bool Edit(
           int cartridgeId,
           string model,
           string description,
           string imageUrl,
           decimal price,
           int colourId,
           int printerId);

        IEnumerable<string> AllModels();

        IEnumerable<CartridgeColourServiceModel> AllColours();

        bool ColourExists(int colourId);
        IEnumerable<CartridgePrinterServiceModel> AllPrinterBrands();
        bool PrinterBrandExists(int printerId);

    }


    
}

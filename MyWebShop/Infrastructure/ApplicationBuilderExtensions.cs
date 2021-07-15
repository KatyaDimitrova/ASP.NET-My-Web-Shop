namespace MyWebShop.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyWebShop.Data;
    using MyWebShop.Data.Models;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        public static void SeedCategories(ApplicationDbContext data)
        {
            if (data.Colours.Any())
            {
                return;
            }

            data.Colours.AddRange(new[]
            {
                new Colour {Name="Black"},
                new Colour {Name="Cyan"},
                new Colour {Name="Magenta"},
                new Colour {Name="Yellow"}
            });

            if (data.Printers.Any())
            {
                return;
            }

            data.Printers.AddRange(new[]
            {
                new Printer {Brand="HP" },
                new Printer {Brand="Brother"},
                new Printer {Brand="Epson"},
                new Printer {Brand="Canon"},
                new Printer {Brand="Lexmark"},
                new Printer {Brand="Samsung"},
                new Printer {Brand="Xerox"}
            });

            data.SaveChanges();
        }

        
    }
}

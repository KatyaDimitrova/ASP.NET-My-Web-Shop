namespace MyWebShop.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyWebShop.Data;
    using MyWebShop.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;
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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@admin.com";
                    const string adminPassword = "admin123";

                    var user = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        //FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}

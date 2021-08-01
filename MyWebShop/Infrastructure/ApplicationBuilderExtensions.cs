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
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        public static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

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
            var userManager = services.GetRequiredService<UserManager<User>>();
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

                    const string adminEmail = "admin@web.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin",
                        City = "Varna",
                        Address = "City of Varna",
                        Phone = "0899999999"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}

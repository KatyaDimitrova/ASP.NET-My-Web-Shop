using Microsoft.EntityFrameworkCore;
using MyWebShop.Models.Cartridges;
namespace MyWebShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyWebShop.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cartridge> Cartridges { get; init; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Printer>Printers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Cartridge>()
                .HasOne(c => c.Colour)
                .WithMany(c => c.Cartridges)
                .HasForeignKey(c => c.ColourId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<Cartridge>()
                .HasOne(c => c.Printer)
                .WithMany(c => c.Cartridges)
                .HasForeignKey(c => c.PrinterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cartridge>()
             .Property(p => p.Price)
             .HasPrecision(10, 2);

            builder.Entity<AllCartridgesViewModel>()
              .Property(p => p.Price)
              .HasPrecision(10, 2);

            base.OnModelCreating(builder);
        }

        public DbSet<MyWebShop.Models.Cartridges.AllCartridgesViewModel> AllCartridgesViewModel { get; set; }
    }
}

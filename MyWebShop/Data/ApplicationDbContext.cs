using Microsoft.EntityFrameworkCore;
using MyWebShop.Models.Cartridges;
namespace MyWebShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyWebShop.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cartridge> Cartridges { get; init; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Printer>Printers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCartridge> OrdersCartridges { get; set; }

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
               .HasMany(p => p.OrderCartridges)
                .WithOne(op => op.Cartridge)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cartridge>()
             .Property(p => p.Price)
             .HasPrecision(10, 2);

            builder.Entity<OrderCartridge>()
              .HasKey(oc => new { oc.OrderId, oc.CartridgeId });

            builder.Entity<Order>()
                .HasMany(o => o.OrderCartridges)
                .WithOne(op => op.Order)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

        }

    }
}

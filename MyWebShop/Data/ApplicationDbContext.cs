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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Cartridge>()
                .HasOne(c => c.Colour)
                .WithMany(c => c.Cartridges)
                .HasForeignKey(c => c.ColourId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<MyWebShop.Models.Cartridges.AllCartridgesViewModel> AllCartridgesViewModel { get; set; }
    }
}

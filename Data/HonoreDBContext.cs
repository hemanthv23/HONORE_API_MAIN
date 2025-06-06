using HONORE_API_MAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace HONORE_API_MAIN.Data
{
    public class HonoreDBContext : DbContext
    {
        public HonoreDBContext(DbContextOptions<HonoreDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Price property to use decimal(18, 2) in the database
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
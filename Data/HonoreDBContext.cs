// Updated Data/HonoreDBContext.cs
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
        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Price property to use decimal(18, 2) in the database
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PinCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CustomerType)
                    .HasConversion<int>();

                // Create unique indexes
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.MobileNumber).IsUnique();

                // Optional string properties with max lengths
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.CommunityName).HasMaxLength(200);
                entity.Property(e => e.ApartmentName).HasMaxLength(200);
                entity.Property(e => e.ContactPersonFirstName).HasMaxLength(100);
                entity.Property(e => e.ContactPersonLastName).HasMaxLength(100);
                entity.Property(e => e.EstablishmentName).HasMaxLength(200);
                entity.Property(e => e.EstablishmentGSTNo).HasMaxLength(15);
                entity.Property(e => e.EstablishmentPhoneNo).HasMaxLength(20);
                entity.Property(e => e.BuildingName).HasMaxLength(200);
                entity.Property(e => e.DoorNo).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

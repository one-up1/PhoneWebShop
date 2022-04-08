using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Models.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace PhoneWebShop.Business
{
    [ExcludeFromCodeCoverage]
    public class DataContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Order> Orders { get; set; }

        private readonly AppSettings _appSettings;
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options, IOptions<AppSettings> appSettings) : base(options) 
        {
            _appSettings = appSettings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>()
                .HasMany(order => order.ProductsPerOrder);


            // modelBuilder.Entity<Phone>()
            //    .HasMany(phone => phone.Orders);

            SeedBrands(modelBuilder);
            SeedPhones(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettings.ConnectionString);
        }

        #region Seeding
        private static void SeedPhones(ModelBuilder builder)
        {
            builder.Entity<Phone>().HasData(new Phone
            {
                Id = 1,
                BrandId = 1,
                Type = "Iphone 6",
                VATPrice = 150,
                Description = "An oldie but goldie.",
                Stock = 1
            });

            builder.Entity<Phone>().HasData(new Phone
            {
                Id = 2,
                BrandId = 1,
                Type = "Iphone 7",
                VATPrice = 299,
                Description = "Better than Iphone 6 but worse than the Samsung Galaxy S7.",
                Stock = 69
            });

            builder.Entity<Phone>().HasData(new Phone
            {
                Id = 3,
                BrandId = 2,
                Type = "Galaxy S7",
                VATPrice = 369,
                Description = "Better than Iphone 7 but worse than the Samsung Galaxy S8.",
                Stock = 32
            });
        }

        private static void SeedBrands(ModelBuilder builder)
        {
            builder.Entity<Brand>().HasData(new Brand
            {
                Id = 1,
                Name = "Apple"
            });

            builder.Entity<Brand>().HasData(new Brand
            {
                Id = 2,
                Name = "Samsung"
            });
        }
        #endregion Seeding
    }
}

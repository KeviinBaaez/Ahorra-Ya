using AhorraYa.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AhorraYa.DataAccess
{
    public class DbDataAccess : IdentityDbContext
    {
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }
        public DbDataAccess() { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public virtual DbSet<PriceOfShop> PriceOfShops { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

    }
}

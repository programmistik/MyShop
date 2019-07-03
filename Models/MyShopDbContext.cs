using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class MyShopDbContext : DbContext
    {

        public MyShopDbContext(DbContextOptions options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }
        public DbSet<Stock> Stock { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductSizes>()
                .HasKey(ps => new { ps.ProductId, ps.SizeId });

            modelBuilder.Entity<ProductSizes>()
                .HasOne(ps => ps.Product)
                .WithMany(b => b.AvalableSizes)
                .HasForeignKey(ps => ps.SizeId);

            modelBuilder.Entity<ProductSizes>()
                .HasOne(ps => ps.Size)
                .WithMany(c => c.AvalableSizes)
                .HasForeignKey(ps => ps.SizeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

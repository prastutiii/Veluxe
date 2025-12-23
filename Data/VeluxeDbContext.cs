using Microsoft.EntityFrameworkCore;
using Veluxe.Models;

namespace Veluxe.Data
{
    public class VeluxeDbContext: DbContext
    {
        public VeluxeDbContext(DbContextOptions<VeluxeDbContext> options) : base(options)
        {
        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<CartModel> Cart { get; set; }
        public DbSet<CartProductModel> Cart_Products { get; set; }
        public DbSet<OrderDetailModel> Order_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartProductModel>()
                .HasKey(cp => new { cp.cart_id, cp.product_id });

            modelBuilder.Entity<OrderDetailModel>()
                .HasKey(od => new { od.order_id, od.product_id });
        }

    }

}

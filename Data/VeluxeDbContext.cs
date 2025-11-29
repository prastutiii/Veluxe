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
    }
}

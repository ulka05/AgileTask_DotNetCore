using AgileInfoTask.Model;
using Microsoft.EntityFrameworkCore;

namespace AgileInfoTask.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<ProductModel>Products{ get; set; }
    }
}

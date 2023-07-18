using Microsoft.EntityFrameworkCore;
using MyWebSite.Models;

namespace MyWebSite.Data
{
    public class NailStoreContext : DbContext
    {
        public NailStoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<PurchasedOrder> Orders { get; set; }
    }
}

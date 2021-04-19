using System.Data.Common;
using System.Configuration;
using System.Data.Entity;

namespace ISP.DatabaseAccess.DataAccess
{
    public class LaptopDbContext : DbContext
    {
        public LaptopDbContext() : base("name=LaptopDbContext")
        {
        }

        public DbSet<LaptopsDto> Laptops { get; set; }
    }
}
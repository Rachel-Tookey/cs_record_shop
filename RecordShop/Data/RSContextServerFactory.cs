using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; 

namespace RecordShop.Data
{
    public class RSContextServerFactory : IDesignTimeDbContextFactory<RecordShopContextSqlServer>
    {
        public RecordShopContextSqlServer CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__RecordShopFinal");

            var optionsBuilder = new DbContextOptionsBuilder<RecordShopContextSqlServer>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RecordShopContextSqlServer(optionsBuilder.Options);
        }
    }
}

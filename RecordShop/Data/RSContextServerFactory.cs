using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; 

namespace RecordShop.Data
{
    public class RSContextServerFactory : IDesignTimeDbContextFactory<RecordShopContextSqlServer>
    {
        public RecordShopContextSqlServer CreateDbContext(string[] args)
        {

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<RecordShopContextSqlServer>()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<RecordShopContextSqlServer>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RecordShopContextSqlServer(optionsBuilder.Options);
        }
    }
}

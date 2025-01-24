using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecordShop.Data;
using RecordShop.HealthChecks;
using RecordShop.Repository;
using RecordShop.Services;
using System.Text.Json.Serialization;

namespace RecordShop.Settings
{
    public class StartupProduction(IConfiguration configuration)
    {

        public IConfiguration configuration { get; } = configuration; 

        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__RecordShopFinal");  
            services.AddDbContext<RecordShopContextSqlServer>(options => options.UseSqlServer(connectionString));
            services.AddHealthChecks().AddCheck("Db-check", new SqlConnectionHealthCheckProduction(connectionString),HealthStatus.Unhealthy,new string[] { "orderingdb" });
            services.AddScoped<IAlbumRepository, AlbumRepositoryProd>();
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IArtistService, ArtistService>();
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.UseEndpoints(e => e.MapControllers());

        }

    }
}

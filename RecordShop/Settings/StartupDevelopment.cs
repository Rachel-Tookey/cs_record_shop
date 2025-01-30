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
    public class StartupDevelopment(IConfiguration configuration)
    {

        public IConfiguration configuration { get; } = configuration; 


        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString = "Data Source=:memory:";
            var sqliteConnection = new SqliteConnection(connectionString);
            sqliteConnection.Open();
            services.AddDbContext<IDbContext, RecordShopContextSqlite>(options =>  options.UseSqlite(sqliteConnection));
            services.AddHealthChecks().AddCheck("Db-check", new SqlConnectionHealthCheck(connectionString), HealthStatus.Unhealthy, new string[] { "orderingdb" });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
        }


        public void Configure(IApplicationBuilder app)
        {
           app.UseSwagger();
           app.UseSwaggerUI();
           app.UseRouting();
           app.UseHttpsRedirection();
           app.UseAuthorization();
           app.UseHealthChecks("/health");
           app.UseEndpoints(e => e.MapControllers());

        }

    }
}

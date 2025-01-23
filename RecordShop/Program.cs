
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.HealthChecks;
using RecordShop.Repository;
using RecordShop.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace RecordShop
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            string connectionString = "";

            if (builder.Environment.IsDevelopment())
            {
                connectionString = builder.Configuration.GetConnectionString("DEVELOPMENT");
                
                var sqliteConnection = new SqliteConnection(connectionString);
                
                sqliteConnection.Open();


                builder.Services.AddDbContext<RecordShopContextSqlite>(options =>
                {
                    options.UseSqlite(sqliteConnection);
                });

                builder.Services.AddHealthChecks() 
                    .AddCheck("Db-check", new SqlConnectionHealthCheck(connectionString),
                    HealthStatus.Unhealthy,
                    new string[] { "orderingdb" }); 

            }
            else if (builder.Environment.IsProduction())
            {

                connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__RecordShopFinal");
                builder.Services.AddDbContext<RecordShopContextSqlServer>(options => options.UseSqlServer(connectionString));

                builder.Services.AddHealthChecks()
                        .AddCheck("Db-check", new SqlConnectionHealthCheckProduction(connectionString),
                        HealthStatus.Unhealthy,
                        new string[] { "orderingdb" });
            }



            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IArtistService, ArtistService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseHealthChecks("/health"); 

            app.MapControllers();

            app.Run();
        }
    }
}

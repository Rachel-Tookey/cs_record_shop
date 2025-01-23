
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
            string Environment = "DEVELOPMENT";

            var builder = WebApplication.CreateBuilder(args);


            if (Environment == "DEVELOPMENT")
            {
                string connectionString = builder.Configuration.GetConnectionString(Environment);
                var sqliteConnection = new SqliteConnection(connectionString);
                sqliteConnection.Open();

                builder.Services.AddDbContext<RecordShopContext>(options =>
                {
                    options.UseSqlite(sqliteConnection);
                });
            }
            else if (Environment == "PRODUCTION")
            {
                string connectionString = builder.Configuration.GetConnectionString(Environment);
                builder.Services.AddDbContext<RecordShopContext>(options => options.UseSqlServer(Environment));
            }



            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IArtistService, ArtistService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks() 
                .AddCheck("Db-check", new SqlConnectionHealthCheck(builder.Configuration.GetConnectionString(Environment)),
                HealthStatus.Unhealthy,
                new string[] { "orderingdb" }); 

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

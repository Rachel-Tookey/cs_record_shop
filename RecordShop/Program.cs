
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.HealthChecks;
using RecordShop.Repository;
using RecordShop.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RecordShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            if (builder.Environment.IsDevelopment())
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                var sqliteConnection = new SqliteConnection(connectionString);
                sqliteConnection.Open();

                builder.Services.AddDbContext<RecordShopContext>(options =>
                {
                    options.UseSqlite(sqliteConnection);
                });
            }
            else if (builder.Environment.IsProduction())
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<RecordShopContext>(options => options.UseSqlServer("DefaultConnection") );
            }



            builder.Services.AddControllers();

            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IArtistService, ArtistService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks().AddCheck<RecordShopHealthCheck>("api_check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "api" }
                )
                .AddCheck("Db-check", new SqlConnectionHealthCheck(builder.Configuration.GetConnectionString("DefaultConnection")),
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

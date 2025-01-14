
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Repository;
using RecordShop.Services;

namespace RecordShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 


            builder.Services.AddDbContext<RecordShopContext>(options =>
            
            options.UseSqlite("Data Source=:memory:")
            
            ); 

            

            builder.Services.AddControllers();

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


            app.MapControllers();

            app.Run();
        }
    }
}


using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.HealthChecks;
using RecordShop.Repository;
using RecordShop.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;
using RecordShop.Settings; 

namespace RecordShop
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    webBuilder.UseStartup<StartupDevelopment>();
                } else
                {
                    webBuilder.UseStartup<StartupProduction>();
                }
            }); 

    }
}

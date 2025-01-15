using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using System.Net.NetworkInformation;


namespace RecordShop.HealthChecks
{
    public class RecordShopHealthCheck : IHealthCheck 
    {

        public Uri Url { get; } 

        public RecordShopHealthCheck(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                throw new ArgumentException("Invalid Url address", nameof(uri));   
            }
            this.Url = uri; 
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(this.Url); 

            if (response.StatusCode < HttpStatusCode.BadRequest)
            {
                return HealthCheckResult.Healthy("Api is running");

            } else
            {
                return HealthCheckResult.Healthy("Api running but not connected to database");
            }

        }

    }
}

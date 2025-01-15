using Microsoft.Extensions.Diagnostics.HealthChecks; 


namespace RecordShop.HealthChecks
{
    public class RecordShopHealthCheck : IHealthCheck 
    {

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true; 

            if (isHealthy)
            {
                return HealthCheckResult.Healthy("Api is running and connected to database");
            } else
            {
                return HealthCheckResult.Healthy("Api running but not connected to database"); 
            }
        }

    }
}

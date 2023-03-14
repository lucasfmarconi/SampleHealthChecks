using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthchecksToolbelt;

public static class DbHealthChecksConfiguration
{

    public static void ConfigureDbHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration["ConnectionStrings:DefaultConnection"], timeout: new System.TimeSpan(0, 0, 60));
    }
}
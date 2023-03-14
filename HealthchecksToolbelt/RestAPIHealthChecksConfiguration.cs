using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthchecksToolbelt;

public static class RestApiHealthChecksConfiguration
{

    public static void ConfigureRestApiHealthCheck(this IServiceCollection services)
    {
        const string urlsConfig = "http://localhost:8080/,http://localhost:8081/";
        var uris = urlsConfig.Split(',');

        foreach (var uri in uris)
        {
            services.AddHealthChecks().AddTypeActivatedCheck<HealthChecks.RestAPICheck>(
                $"restapi_healthcheck_{uri}",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "RestAPICheck" }, new object[] { uri });
        }
    }
}
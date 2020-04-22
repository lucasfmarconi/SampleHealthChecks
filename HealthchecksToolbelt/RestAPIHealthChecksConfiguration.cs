using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthchecksToolbelt
{
    public class RestAPIHealthChecksConfiguration
    {
        public IConfiguration Configuration { get; }

        public RestAPIHealthChecksConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IServiceCollection services)
        {
            var urlsConfig = "http://localhost:8080/,http://localhost:8081/";
            string[] uris = urlsConfig?.Split(',');
            //string[] uris = 
            for (int i = 0; i < uris?.Length; i++)
            {
                services.AddHealthChecks().AddTypeActivatedCheck<HealthChecks.RestAPICheck>(
                    $"restapi_healthcheck_{uris[i]}",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "RestAPICheck" }, new object[] { uris[i] });
            }
        }
    }
}

using HealthchecksToolbelt.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks;

namespace HealthchecksToolbelt
{
    public class MemoryHealthChecksConfiguration
    {
        public IConfiguration Configuration { get; }

        public MemoryHealthChecksConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<MemoryHealthCheck>(
                "MemoryHealthCheck",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "MemoryHealthChecks" });
        }
    }
}
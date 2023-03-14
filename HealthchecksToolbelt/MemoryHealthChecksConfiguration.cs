using HealthchecksToolbelt.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthchecksToolbelt;

public static class MemoryHealthChecksConfiguration
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<MemoryHealthCheck>(
            "MemoryHealthCheck",
            failureStatus: HealthStatus.Degraded,
            tags: new[] { "MemoryHealthChecks" });
    }
}
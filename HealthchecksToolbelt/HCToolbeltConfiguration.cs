using System;
using HealthchecksToolbelt.Publisher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthchecksToolbelt;

public static class HcToolbeltConfiguration
{
    public static void ConfigureHealthCheckLogReadinessPublisher(this IServiceCollection services)
    {
        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(10);
        });

        services.AddSingleton<IHealthCheckPublisher, ReadinessPublisher>();
    }
}
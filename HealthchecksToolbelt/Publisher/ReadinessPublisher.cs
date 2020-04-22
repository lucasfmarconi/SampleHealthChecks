using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HealthchecksToolbelt.Publisher
{
    public class ReadinessPublisher : IHealthCheckPublisher
    {
        private readonly ILogger _logger;

        public ReadinessPublisher(ILogger<ReadinessPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status == HealthStatus.Healthy)
            {
                _logger.LogInformation("{Timestamp} Readiness Probe Status: {Result}",
                    DateTime.UtcNow, report.Status);
            }
            else
            {
                _logger.LogError("{Timestamp} Readiness Probe Status: {Result}",
                        DateTime.UtcNow, report.Status);

                report.Entries.Where(e => e.Value.Status.Equals(HealthStatus.Unhealthy))
                    .ToList().ForEach(e =>
                    {
                        _logger.LogError("{HCkey} Exception: {Exception}",
                            e.Key, e.Value.Exception);
                    });
            }

            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }
    }
}

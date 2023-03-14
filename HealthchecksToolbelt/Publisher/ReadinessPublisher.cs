using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HealthchecksToolbelt.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HealthchecksToolbelt.Publisher;

public class ReadinessPublisher : IHealthCheckPublisher
{
    private readonly ILogger _logger;
    private readonly Serilog.ILogger _serilog;

    public ReadinessPublisher(ILogger<ReadinessPublisher> logger, IConfiguration configuration)
    {
        _logger = logger;
        _serilog = new SerilogUtil().CreateLogger();
    }
    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        if (report.Status == HealthStatus.Healthy)
        {
            _serilog.Information("{Timestamp} Readiness Probe Status: {@Result}",
                DateTime.UtcNow, report.Status);
        }
        else
        {
            _serilog.Error("{Timestamp} Readiness Probe Status: {@Result}",
                DateTime.UtcNow, report.Status);

            ReportEntriesAsError(report);
        }

        cancellationToken.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    private void ReportEntriesAsError(HealthReport report)
    {
        report.Entries.Where(e => e.Value.Status.Equals(HealthStatus.Unhealthy))
            .ToList().ForEach(e =>
            {
                _serilog.Error("{HCkey} Exception: {@Exception}",
                    e.Key, e.Value.Exception);
            });
    }
}
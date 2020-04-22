using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthchecksToolbelt.HealthChecks
{
    public class RestAPICheck : IHealthCheck
    {

        private string _uriToCheck { get; }

        public RestAPICheck(string uriToCheck)
        {
            _uriToCheck = uriToCheck;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = await new Utils.RestClientUtil()
                .ProcessRestApiCallAsync(HttpMethod.Get, _uriToCheck);

            if (healthCheckResultHealthy.StatusCode.Equals(HttpStatusCode.OK))
            {
                return HealthCheckResult.Healthy("Healthy");
            }

            return HealthCheckResult.Unhealthy("Unhealthy");
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthchecksToolbelt
{
    public class DbHealthChecksConfiguration
    {
        public IConfiguration Configuration { get; }

        public DbHealthChecksConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"], timeout: new System.TimeSpan(0, 0, 60));
        }
    }
}

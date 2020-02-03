using CenterEdge.ReleaseNotes.Adapters;
using CenterEdge.ReleaseNotes.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace CenterEdge.ReleaseNotes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReleaseNotes(this IServiceCollection services)
        {
            services
                .AddTransient<IDeploymentRepository, DeploymentRepository>();

            return services;
        }
    }
}
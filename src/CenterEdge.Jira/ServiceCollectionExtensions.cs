using System;
using CenterEdge.JiraLibrary.Adapters;
using CenterEdge.JiraLibrary.Data;
using CenterEdge.JiraLibrary.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CenterEdge.JiraLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJira(this IServiceCollection services)
        {
            return services.AddJira((Action<JiraOptions>)null);
        }

        public static IServiceCollection AddJira(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddJira(gitHubOptions => configuration.GetSection(JiraOptions.JiraConfigurationSection).Bind(gitHubOptions));
        }

        public static IServiceCollection AddJira(
           this IServiceCollection services,
           Action<JiraOptions> gitHubOptionsAction)
        {
            if (gitHubOptionsAction != null)
            {
                services.Configure(gitHubOptionsAction);
            }

            services.AddTransient<IJiraRepository, JiraRepository>();

            return services;
        }
    }
}

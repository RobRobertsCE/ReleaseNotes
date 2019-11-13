using System;
using CenterEdge.SourceControl.GitHub.Adapters;
using CenterEdge.SourceControl.GitHub.Data;
using CenterEdge.SourceControl.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CenterEdge.SourceControl.GitHub
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGitHub(this IServiceCollection services)
        {
            return services.AddGitHub((Action<GitHubOptions>)null);
        }

        public static IServiceCollection AddGitHub(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddGitHub(gitHubOptions => configuration.GetSection(GitHubOptions.GitHubConfigurationSection).Bind(gitHubOptions));
        }

        public static IServiceCollection AddGitHub(
           this IServiceCollection services,
           Action<GitHubOptions> gitHubOptionsAction)
        {
            if (gitHubOptionsAction != null)
            {
                services.Configure(gitHubOptionsAction);
            }

            services.AddTransient<IWorkItemRepository, WorkItemRepository>();

            return services;
        }
    }
}

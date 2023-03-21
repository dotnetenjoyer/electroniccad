using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop.Infrastructure.DependencyInjection;

/// <summary>
/// Infrastructure module.
/// </summary>
internal static class InfrastructureModule
{
    /// <summary>
    /// Register infrastructure.
    /// </summary>
    public static void Register(IServiceCollection services)
    {
        services.AddTransient<IRecentProjectsService>((services) =>
        {
            var applicationDataFolder = CompositionRoot.GetApplicationDataFolder();
            return new RecentProjectService(applicationDataFolder);
        });
    }
}

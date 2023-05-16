using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Implementations.Services;
using ElectronicCad.Infrastructure.Implementations.Services.Projects;
using ElectronicCad.MVVM.Properties.Implementation;
using ElectronicCad.MVVM.Properties.Implementation.CustomSections;
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
        services.AddTransient<IFolderPicker, FolderPicker>();
        services.AddTransient<IFilePicker, FilePicker>();

        services.AddTransient<IProjectSaver, ProjectSaver>();
        services.AddSingleton<ICurrentProjectProvider, CurrentProjectProvider>();
        services.AddSingleton<ISelectionService, SelectionService>();   

        services.AddTransient<IRecentProjectsService>(services =>
        {
            var applicationDataFolder = CompositionRoot.GetApplicationDataFolder();
            return new RecentProjectService(applicationDataFolder);
        });

        services.AddScoped<PropertyObjectFactory>();
        services.AddScoped<ICustomSectionFactoriesFactory, CustomSectionFactoriesFactory>();
    }
}

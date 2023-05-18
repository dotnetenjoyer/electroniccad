using Microsoft.Extensions.DependencyInjection;
using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;
using ElectronicCad.Infrastructure.Implementations.Services;
using ElectronicCad.Infrastructure.Implementations.Services.Projects;
using ElectronicCad.Infrastructure.Implementations.Services.Projects.Diagrams;
using ElectronicCad.MVVM.Properties.Implementation;
using ElectronicCad.MVVM.Properties.Implementation.CustomSections;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;

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
        services.AddScoped<IFolderPicker, FolderPicker>();
        services.AddScoped<IFilePicker, FilePicker>();

        services.AddTransient<IProjectSaver, ProjectSaver>();
        services.AddSingleton<IOpenProjectProvider, OpentProjectProvider>();
        
        services.AddSingleton(services => 
            (IOpenDiagramProvider)ActivatorUtilities.CreateInstance(services, typeof(ProjectDiagramOpener)));
        services.AddSingleton(services => 
            (IDiagramOpener)ActivatorUtilities.CreateInstance(services, typeof(ProjectDiagramOpener)));

        services.AddTransient<IRecentProjectsService>(services =>
        {
            var applicationDataFolder = CompositionRoot.GetApplicationDataFolder();
            return new RecentProjectService(applicationDataFolder);
        });

        services.AddSingleton<ISelectionService, SelectionService>();
        services.AddScoped<PropertyObjectFactory>();
        services.AddScoped<ICustomSectionFactoriesFactory, CustomSectionFactoriesFactory>();

        services.AddSingleton<IApplicationLocalStorage>(service =>
        {
            var applicationDataFolder = CompositionRoot.GetApplicationDataFolder();
            return new ApplicationLocalStorage(applicationDataFolder);
        });
    }
}

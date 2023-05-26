using Microsoft.Extensions.DependencyInjection;
using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;
using ElectronicCad.Infrastructure.Implementations.Services;
using ElectronicCad.Infrastructure.Implementations.Services.Projects;
using ElectronicCad.Infrastructure.Implementations.Services.Projects.Diagrams;
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
        services.AddSingleton<IFolderPicker, FolderPicker>();
        services.AddSingleton<IFilePicker, FilePicker>();
        services.AddSingleton<ISelectionService, SelectionService>();

        services.AddTransient<IProjectSaver, ProjectSaver>();
        services.AddSingleton<IOpenProjectProvider, OpentProjectProvider>();
        
        services.AddSingleton(services =>
        {
            var projectDiagramOpener = (IOpenDiagramProvider)ActivatorUtilities.CreateInstance(services, typeof(ProjectDiagramOpener));
            return projectDiagramOpener;
        });

        services.AddSingleton(services =>
        {
            var projectDiagramOpener = (IDiagramOpener)ActivatorUtilities.CreateInstance(services, typeof(ProjectDiagramOpener));
            return projectDiagramOpener;
        });

        services.AddTransient<IRecentProjectsService>(services =>
        {
            var applicationDataFolder = CompositionRoot.GetApplicationDataFolder();
            return new RecentProjectService(applicationDataFolder);
        });
        
        services.AddSingleton<IApplicationLocalStorage>(service =>
        {
            var applicationDataFolder = CompositionRoot.GetApplicationDataFolder();
            return new ApplicationLocalStorage(applicationDataFolder);
        });

        services.AddSingleton<ISizePrototypesStorage, SizePrototypeStorage>();
    }
}

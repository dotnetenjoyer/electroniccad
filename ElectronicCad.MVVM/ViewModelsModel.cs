using Microsoft.Extensions.DependencyInjection;
using ElectronicCad.MVVM.Properties.Implementation;
using ElectronicCad.MVVM.Properties.Implementation.CustomSections;
using ElectronicCad.MVVM.ViewModels.Common;

namespace ElectronicCad.MVVM;

/// <summary>
/// Viwe models module.
/// </summary>
public static class ViewModelsModel
{
    /// <summary>
    /// Register view models services.
    /// </summary>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<DiagramsContextMenuFactory>();

        services.AddScoped<PropertyObjectFactory>();
        services.AddScoped<ICustomSectionFactoriesFactory, CustomSectionFactoriesFactory>();
    }
}
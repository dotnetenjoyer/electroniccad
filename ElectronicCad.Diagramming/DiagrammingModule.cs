using ElectronicCad.Diagramming.Services;
using ElectronicCad.Domain.Geometry.Typography;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Diagramming;

/// <summary>
/// Diagramming module.
/// </summary>
public static class DiagrammingModule
{
    /// <summary>
    /// Register diagramming services.
    /// </summary>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IFontFamilyStorage, FontFamilyStorage>();
    }
}
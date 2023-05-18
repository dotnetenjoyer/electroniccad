using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates configuration for project diagram.
/// </summary>

internal class ProjectDiagramProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectDiagramProfile()
    {
        CreateConfiguration<ProjectDiagramPropertiesProxy>();
    }
}
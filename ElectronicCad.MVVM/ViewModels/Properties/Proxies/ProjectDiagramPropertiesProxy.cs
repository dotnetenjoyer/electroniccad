using ElectronicCad.Domain.Workspace;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Contains proxy project diagram properties.
/// </summary>
public class ProjectDiagramPropertiesProxy : BaseProxy<ProjectDiagram>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Project diagram.</param>
    public ProjectDiagramPropertiesProxy(ProjectDiagram diagram) : base(diagram)
    {
    }

    /// <inheritdoc />
    public override void UpdateEntity()
    {
    }

    /// <inheritdoc />
    public override void UpdateFromEntity()
    {
    }
}
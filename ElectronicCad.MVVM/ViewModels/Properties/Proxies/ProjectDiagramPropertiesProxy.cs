using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Contains proxy project diagram properties.
/// </summary>
public class ProjectDiagramPropertiesProxy : BaseProxy<ProjectDiagram>, ILayoutGridProxy
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Project diagram.</param>
    public ProjectDiagramPropertiesProxy(ProjectDiagram diagram) : base(diagram)
    {
    }

    /// <inhertidoc />
    public IEnumerable<LayoutGrid> LayoutGrids { get; set; }

    /// <inheritdoc />
    public override void UpdateEntity()
    {
    }

    /// <inheritdoc />
    public override void UpdateFromEntity()
    {
        LayoutGrids = Source.GeometryDiagram.LayoutGrids;
    }
}
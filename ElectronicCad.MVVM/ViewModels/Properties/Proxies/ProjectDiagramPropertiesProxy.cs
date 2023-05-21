using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Contains proxy project diagram properties.
/// </summary>
public class ProjectDiagramPropertiesProxy : BaseProxy<ProjectDiagram>, ILayoutGridProxy
{
    /// <inhertidoc />
    public IEnumerable<LayoutGrid> LayoutGrids { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Project diagram.</param>
    public ProjectDiagramPropertiesProxy(ProjectDiagram diagram) : base(diagram)
    {
    }

    /// <inhertidoc />
    public void AddLayoutGrid(LayoutGrid layoutGrid)
    {
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.AddLayoutGrid(layoutGrid);
        Source.GeometryDiagram.CompleteModification();
    }

    /// <inhertidoc />
    public void UpdateLayoutGrid(LayoutGrid updatedLayoutGrid)
    {
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.UpdateLayoutGrid(updatedLayoutGrid);
        Source.GeometryDiagram.CompleteModification();
    }

    /// <inhertidoc />
    public void RemoveLayoutGrid(LayoutGrid layoutGrid)
    {
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.RemoveLayoutGrid(layoutGrid);
        Source.GeometryDiagram.CompleteModification();
    }

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
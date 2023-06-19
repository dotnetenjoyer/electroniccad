using ElectronicCad.Domain.Workspace;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;
using ElectronicCad.Domain.Exceptions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Contains proxy project diagram properties.
/// </summary>
public class ProjectDiagramPropertiesProxy : NotificationPropertiesProxy<ProjectDiagram>, ILayoutGridProxy, ISizeProxy
{
    /// <summary>
    /// Project diagram name.
    /// </summary>
    public string Name { get; set; }
    
    /// <inhertidoc />
    public IEnumerable<LayoutGrid> LayoutGrids { get; set; }

    /// <inhertidoc />
    public Size Size { get; set; }

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
    public override void UpdateFromSource()
    {
        Name = Source.Name;
        LayoutGrids = Source.GeometryDiagram.LayoutGrids;
        Size = Source.GeometryDiagram.Size;
    }

    /// <inheritdoc />
    public override void UpdateSource()
    {
        Source.PropertyChanged -= HandleSourcePropertyChange;
        
        using var scope = Source.GeometryDiagram.StartModificationScope();
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.Size = Size;
        Source.Rename(Name);
        Source.GeometryDiagram.CompleteModification();
    
        Source.PropertyChanged += HandleSourcePropertyChange;
    }
}
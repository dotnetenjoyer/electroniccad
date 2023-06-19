using ElectronicCad.Domain.Workspace;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Layouts;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;
using ElectronicCad.Domain.Exceptions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Contains proxy project diagram properties.
/// </summary>
public class ProjectDiagramPropertiesProxy : NotificationPropertiesProxy<ProjectDiagram>, ILayoutProxy, ISizeProxy
{
    /// <summary>
    /// Project diagram name.
    /// </summary>
    public string Name { get; set; }
    
    /// <inhertidoc />
    public IEnumerable<Layout> Layouts { get; set; }

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
    public void AddLayout(Layout layoutGrid)
    {
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.AddLayout(layoutGrid);
        Source.GeometryDiagram.CompleteModification();
    }

    /// <inhertidoc />
    public void UpdateLayout(Layout updatedLayoutGrid)
    {
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.UpdateLayout(updatedLayoutGrid);
        Source.GeometryDiagram.CompleteModification();
    }

    /// <inhertidoc />
    public void RemoveLayout(Layout layoutGrid)
    {
        Source.GeometryDiagram.StartModification();
        Source.GeometryDiagram.RemoveLayout(layoutGrid);
        Source.GeometryDiagram.CompleteModification();
    }

    /// <inheritdoc />
    public override void UpdateFromSource()
    {
        Name = Source.Name;
        Layouts = Source.GeometryDiagram.Layouts;
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
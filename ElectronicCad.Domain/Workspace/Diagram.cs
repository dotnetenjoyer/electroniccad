using ElectronicCad.Domain.Common;

namespace ElectronicCad.Domain.Workspace;

/// <summary>
/// Woorkbook diagram base.
/// </summary>
public abstract class Diagram : DomainObservableObject
{
    /// <summary>
    /// Diagram id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Diagram name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Geometry diagram.
    /// </summary>
    public Geometry.Diagram GeometryDiagram { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="geometryDiagram"></param>
    public Diagram(string name, Geometry.Diagram geometryDiagram)
    {
        Id = Guid.NewGuid();
        Name = name;
        GeometryDiagram = geometryDiagram;
    }
}

public class ComponentDiagram : Diagram
{
    public ComponentDiagram(string name, Geometry.Diagram geometryDiagram) : base(name, geometryDiagram)
    {
        
    }
}
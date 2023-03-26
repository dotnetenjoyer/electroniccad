namespace ElectronicCad.Domain.Workspace;

/// <summary>
/// Woorkbook diagram base.
/// </summary>
public abstract class Diagram
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
namespace ElectronicCad.Domain.Workspace;

/// <summary>
/// Component diagram.
/// </summary>
public class ComponentDiagram : Diagram
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Diagram name.</param>
    /// <param name="geometryDiagram">Geometry diagram.</param>
    public ComponentDiagram(string name, Geometry.Diagram geometryDiagram) : base(name, geometryDiagram)
    {
    }
}
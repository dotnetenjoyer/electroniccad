namespace ElectronicCad.Domain.Workspace;

/// <summary>
/// Project diagram.
/// </summary>
public class ProjectDiagram : Diagram
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Diagram name.</param>
    /// <param name="geometryDiagram">Geometry diagram.</param>
    public ProjectDiagram(string name, Geometry.Diagram geometryDiagram) : base(name, geometryDiagram)
    {
    }
}
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
    /// <param name="name">Diagram name.</param>
    /// <param name="geometryDiagram">Geometry diagram.</param>
    public Diagram(string name, Geometry.Diagram geometryDiagram)
    {
        Id = Guid.NewGuid();
        Name = name;
        GeometryDiagram = geometryDiagram;
    }
}

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
    public Guid Id { get; internal set; }

    /// <summary>
    /// Diagram name.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Date of diagram creation.
    /// </summary>
    public DateTime CreatedAt { get; internal set; }

    /// <summary>
    /// Geometry diagram.
    /// </summary>
    public Geometry.Diagram GeometryDiagram { get; init; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    internal Diagram()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Diagram name.</param>
    /// <param name="geometryDiagram">Geometry diagram.</param>
    public Diagram(string name, Geometry.Diagram geometryDiagram)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.Now;
        GeometryDiagram = geometryDiagram;
    }
}

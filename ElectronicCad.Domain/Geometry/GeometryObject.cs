using System.Numerics;

namespace ElectronicCad.Domain.Geometry;

public abstract class GeometryObject
{
    /// <summary>
    /// Geometry object id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public virtual string Name { get; init; }

    /// <summary>
    /// Control points.
    /// </summary>
    public Vector2[] ControlPoints { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GeometryObject()
    {
        Id = Guid.NewGuid();
    }
}
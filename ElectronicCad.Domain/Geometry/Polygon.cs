namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Polygon geometry object.
/// </summary>
public class Polygon : ContentGeometry
{
    /// <inheritdoc/>
    public override string Name { get; init; } = nameof(Polygon);

    /// <summary>
    /// Constructor.
    /// </summary>
    public Polygon(params Point[] points) : base(points)
    {
    }
}
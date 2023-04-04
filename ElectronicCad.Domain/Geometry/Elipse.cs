namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Ellipse.
/// </summary>
public class Ellipse : ContentGeometry
{
    /// <inheritdoc/>
    public override string Name { get; init; } = nameof(Ellipse);

    /// <summary>
    /// Constructor.
    /// </summary>
    public Ellipse(params Point[] points) : base(points)
    {
    }
}

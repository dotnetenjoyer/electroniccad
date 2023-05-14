namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Ellipse.
/// </summary>
public class Ellipse : ContentGeometry
{
    /// <inheritdoc/>
    public override string Name { get; init; } = nameof(Ellipse);

    /// <summary>
    /// The x radius of the ellipse. 
    /// </summary>
    public double RadiusX => BoundingBox.Width / 2;

    /// <summary>
    /// The y radius of the ellipse.
    /// </summary>
    public double RadiusY => BoundingBox.Height / 2;
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="center">The center of the ellipse.</param>
    /// <param name="radius">The radius of the ellipse</param>
    public Ellipse(Point center, double radius, bool isTemporary = false) : base(center, radius * 2, radius * 2, isTemporary)
    {
    }

    /// <summary>
    /// Set center point and radiuses of the ellipse.
    /// </summary>
    /// <param name="centerPoint">Center point.</param>
    /// <param name="radiusX">Radius x.</param>
    /// <param name="radiusY">Radius y.</param>
    public void SetCenterAndRadius(Point centerPoint, double radiusX, double radiusY)
    {
        var width = radiusX * 2;
        var height = radiusY * 2;
        SetCenterAndSize(centerPoint, width, height);
    }
}

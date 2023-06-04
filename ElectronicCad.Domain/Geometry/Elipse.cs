namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Ellipse.
/// </summary>
public class Ellipse : ContentGeometry
{
    /// <inheritdoc/>
    public override string Name { get; internal set; } = nameof(Ellipse);

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
    /// Cloning construtor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public Ellipse(Ellipse cloneFrom) : base(cloneFrom)
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

    /// <inheritdoc />
    public override bool CheckHit(Point point)
    {
        var delta = CenterPoint - point;
        return 1 >= Math.Pow(delta.X, 2) / Math.Pow(RadiusX, 2) 
            + Math.Pow(delta.Y, 2) / Math.Pow(RadiusY, 2);
    }
}

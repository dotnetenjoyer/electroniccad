namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Represent geometry object that has base 
/// </summary>
public abstract class ContentGeometry : GeometryObject
{
    /// <summary>
    /// Index of left top control point.
    /// </summary>
    public const int LeftTopPointIndex = 0;

    /// <summary>
    /// Index of rigth top control point.
    /// </summary>
    public const int RigthTopPointIndex = 1;
    
    /// <summary>
    /// Index of rigth bottom control point.
    /// </summary>
    public const int RigthBottomPointIndex = 2;
    
    /// <summary>
    /// Index of left bottom control point.
    /// </summary>
    public const int LeftBottomPointIndex = 3;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ContentGeometry()
    {
        controlPoints = new Point[4];       
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="points">Initial control points.</param>
    public ContentGeometry(Point[] points) : this()
    {
        SetControlPoints(points);
    }

    /// <inheritdoc />
    public override void UpdateBoundingBox(float x, float y, float width, float height)
    {
        ValidateModification();

        var halfWidth = width / 2;
        var halfHeight = height / 2;
        SetControlPoint(LeftTopPointIndex, x - halfWidth, y - halfHeight);
        SetControlPoint(LeftBottomPointIndex, x - halfWidth, y + halfHeight);
        SetControlPoint(RigthTopPointIndex, x + halfWidth, y - halfHeight);
        SetControlPoint(RigthBottomPointIndex, x + halfWidth, y + halfHeight);
    }
}
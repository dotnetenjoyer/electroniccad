namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Line.
/// </summary>
public class Line : GeometryObject
{
    /// <summary>
    /// Index of first control point.
    /// </summary>
    public const int FirstPointIndex = 0;

    /// <summary>
    /// Index of second control point.
    /// </summary>
    public const int SecondPointIndex = 1;

    /// <inheritdoc/>
    public override string Name { get; init; } = nameof(Line);

    /// <summary>
    /// First point of the line.
    /// </summary>
    public Point FirstPoint => ControlPoints[FirstPointIndex];

    /// <summary>
    /// Second point of the line.
    /// </summary>
    public Point SecondPoint => ControlPoints[SecondPointIndex];

    /// <summary>
    /// Constructor.
    /// </summary>
    public Line(bool isTemporary) : base(isTemporary)
    {
        controlPoints = new Point[2];
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="points"></param>
    public Line(Point[] points, bool isTemporary = false) : this(isTemporary)
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            controlPoints[i] = points[i];
        }

        RecalculateBoundingBox();
    }

    /// <summary>
    /// Clonning constructor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public Line(Line cloneFrom) : base(cloneFrom)
    {
    }

    /// <inheritdoc />
    public override bool CheckHit(Point point)  
    {
        var result = (point.X - FirstPoint.X) / (SecondPoint.X - FirstPoint.X) 
            - (point.Y - FirstPoint.Y) / (SecondPoint.Y - FirstPoint.Y);

        var threshold = 0.01d;
        var lineContainsPoint = Math.Abs(result) <= threshold;

        return lineContainsPoint && BoundingBox.Contains(point);
    }
}
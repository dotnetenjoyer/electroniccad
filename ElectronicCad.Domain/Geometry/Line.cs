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
    /// Constructor.
    /// </summary>
    public Line()
    {
        controlPoints = new Point[2];
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="points"></param>
    public Line(params Point[] points) : this()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            controlPoints[i] = points[i];
        }
    }
}
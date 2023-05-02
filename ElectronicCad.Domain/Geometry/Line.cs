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

    /// <inheritdoc />
    public override void UpdateBoundingBox(float centerX, float centerY, float width, float height)
    {
        var firstPoinX = CalculateXPosition(centerX, width, FirstPointIndex, SecondPointIndex);
        var firstPointY = CalculateYPosition(centerY, height, FirstPointIndex, SecondPointIndex);

        var secondPointX = CalculateXPosition(centerX, width, SecondPointIndex, FirstPointIndex);
        var secondPointY = CalculateYPosition(centerY, height, SecondPointIndex, FirstPointIndex);
        
        UpdateControlPoint(FirstPointIndex, firstPoinX, firstPointY);
        UpdateControlPoint(SecondPointIndex, secondPointX, secondPointY);
    }

    private float CalculateXPosition(float centerX, float width, int firstPoint, int secondPoint)
    {
        return ControlPoints[firstPoint].X > ControlPoints[secondPoint].X
            ? centerX + width / 2
            : centerX - width / 2;
    }

    private float CalculateYPosition(float centerY, float height, int firstPoint, int secondPoint)
    {
        return ControlPoints[firstPoint].Y > ControlPoints[secondPoint].Y
            ? centerY + height/ 2
            : centerY - height / 2;
    }
}
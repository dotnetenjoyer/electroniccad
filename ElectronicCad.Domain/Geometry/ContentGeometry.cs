namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Represent geometry object that has base 
/// </summary>
public abstract class ContentGeometry : GeometryObject
{
    /// <summary>
    /// Index of center control point.
    /// </summary>
    public const int CenterPointIndex = 0;

    /// <summary>
    /// Index of left top control point.
    /// </summary>
    public const int LeftTopPointIndex = 1;

    /// <summary>
    /// Index of rigth top control point.
    /// </summary>
    public const int RigthTopPointIndex = 2;
    
    /// <summary>
    /// Index of rigth bottom control point.
    /// </summary>
    public const int RigthBottomPointIndex = 3;
    
    /// <summary>
    /// Index of left bottom control point.
    /// </summary>
    public const int LeftBottomPointIndex = 4;

    /// <summary>
    /// Center point of content geometry.
    /// </summary>
    public Point CenterPoint => ControlPoints[CenterPointIndex];

    /// <summary>
    /// Left top point of content geometry;
    /// </summary>
    public Point LeftTopPoint => ControlPoints[LeftTopPointIndex];

    /// <summary>
    /// Rigth top point of content geometry;
    /// </summary>
    public Point RightTopPoint => ControlPoints[RigthTopPointIndex];

    /// <summary>
    /// Left bottom point of content geometry;
    /// </summary>
    public Point LeftBottomPoint => ControlPoints[LeftBottomPointIndex];

    /// <summary>
    /// Rigth bottom point of content geometry;
    /// </summary>
    public Point RightBottomPoint => ControlPoints[RigthBottomPointIndex];

    /// <summary>
    /// Fill color.
    /// </summary>
    public Color FillColor
    {
        get => fillColor;
        set
        {
            ValidateModification();
            fillColor = value;
        }
    }

    protected Color fillColor = Color.Transparent;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isTemporary">Indicate if geometry object is temporary.</param>
    public ContentGeometry(bool isTemporary = false) : base(isTemporary)
    {
        controlPoints = new Point[5];
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="center">Center point.</param>
    /// <param name="width">Widht.</param>
    /// <param name="height">Height.</param>
    /// <param name="isTemporary">Is geometry object temporary.</param>
    public ContentGeometry(Point center, double width, double height, bool isTemporary = false) : this(isTemporary)
    {
        var widthHalf = width / 2;
        var heightHalf = height / 2;

        var topLeft = new Point(center.X - widthHalf, center.Y - heightHalf);
        var topRight = new Point(center.X + widthHalf, center.Y - heightHalf);
        var bottomLeft = new Point(center.X - widthHalf, center.Y + heightHalf);
        var bottomRight = new Point(center.X + widthHalf, center.Y + heightHalf);

        SetControlPoints(center, topLeft, topRight, bottomLeft, bottomRight);
    }

    /// <summary>
    /// Cloning constructor. 
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public ContentGeometry(ContentGeometry cloneFrom) : base(cloneFrom)
    {
        fillColor = new Color(cloneFrom.FillColor);
    }
}
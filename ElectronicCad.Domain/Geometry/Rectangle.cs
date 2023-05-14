namespace ElectronicCad.Domain.Geometry;

public struct Rectangle
{
    /// <summary>
    /// Start rectangle point.
    /// </summary>
    public Point Start { get; init; }

    /// <summary>
    /// Center rectangle point.
    /// </summary>
    public Point Center { get; init; }

    /// <summary>
    /// End rectangle point.
    /// </summary>
    public Point End { get; init; }

    /// <summary>
    /// Rectangle width.
    /// </summary>
    public double Width { get; init; }

    /// <summary>
    /// Rectangle height.
    /// </summary>
    public double Height { get; init; }

    /// <summary>
    /// Empty rectangle instance.
    /// </summary>
    public static Rectangle Empty => new Rectangle(new Point(0, 0), 0, 0);

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    /// <param name="width">Rectangle width.</param>
    /// <param name="height">Rectangle height.</param>
    public Rectangle(Point startPoint, double width, double height)
    {
        Width = width;
        Height = height;
        Start = startPoint;
        End = new Point(Start.X + width, Start.Y + height);
        Center = new Point(Start.X + width / 2, Start.Y + height / 2);
    }

    /// <summary>
    /// Check point containing in the rectangle.
    /// </summary>
    /// <param name="point">Point to check.</param>
    /// <returns>true if contains.</returns>
    public bool Contains(Point point)
    {
        return point > Start && point < End;
    }
}
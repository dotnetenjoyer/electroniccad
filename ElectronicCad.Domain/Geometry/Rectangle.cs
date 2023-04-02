namespace ElectronicCad.Domain.Geometry;

public class Rectangle
{
    /// <summary>
    /// X value.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Y value.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Rectangle width.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// Rectangle height.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Empty rectangle instance.
    /// </summary>
    public static Rectangle Empty => new Rectangle(0, 0, 0, 0);

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    /// <param name="width">Rectangle width.</param>
    /// <param name="height">Rectangle height.</param>
    public Rectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Check point containing in the rectangle.
    /// </summary>
    /// <param name="point">Point to check.</param>
    /// <returns>true if contains.</returns>
    public bool Contains(Point point)
    {
        return point.X > X && point.Y > Y 
            && point.X < X + Width && point.Y < Y + Height;
    }
}
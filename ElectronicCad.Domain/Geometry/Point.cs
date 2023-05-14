namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Representation of a two demension point on a plane.
/// </summary>
public struct Point
{
    /// <summary>
    /// X coordinate value.
    /// </summary>
    public double X 
    {
        get => x;
        private set => x = value;
    }

    private double x;

    /// <summary>
    /// Y coordinate value.
    /// </summary>
    public double Y 
    {
        get => y;
        private set => y = value; 
    }

    private double y;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">X coordinate value.</param>
    /// <param name="y">Y coordinate value.</param>
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Override the difference operator.
    /// </summary>
    /// <param name="first">First point.</param>
    /// <param name="second">Second point.</param>
    /// <returns>Point that describe difference.</returns>
    public static Point operator -(Point first, Point second)
    {
        return new Point(first.X - second.X, first.Y - second.Y);
    }

    /// <summary>
    /// Override the sum operator.
    /// </summary>
    /// <param name="first">First point.</param>
    /// <param name="second">Second point.</param>
    /// <returns>Point that describe sum.</returns>
    public static Point operator +(Point first, Point second)
    {
        return new Point(first.X + second.X, first.Y + second.Y);
    }

    /// <summary>
    /// Override the compare operator.
    /// </summary>
    /// <param name="first">First point.</param>
    /// <param name="second">Second point.</param>
    /// <returns>True if first point greater than rigth.</returns>
    public static bool operator >(Point first, Point second)
    {
        return first.X > second.X && first.Y > second.Y;
    }

    /// <summary>
    /// Override the compare operator.
    /// </summary>
    /// <param name="first">First point.</param>
    /// <param name="second">Second point.</param>
    /// <returns>Point that describe sum.</returns>
    public static bool operator <(Point first, Point second)
    {
        return first.X < second.X && first.Y < second.Y;
    }

    /// <summary>
    /// Set new coordinate values.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    internal void SetValues(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Calculates the length of the point.
    /// </summary>
    /// <returns>Length.</returns>
    public double CalculateLength()
    {
        return Math.Sqrt(x * x + y * y);
    } 
}
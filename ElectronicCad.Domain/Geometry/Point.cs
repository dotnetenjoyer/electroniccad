using System.Data.Common;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Representation of a two demension point on a plane.
/// </summary>
public struct Point
{
    /// <summary>
    /// X coordinate value.
    /// </summary>
    public float X 
    {
        get => x;
        private set => x = value;
    }

    private float x;

    /// <summary>
    /// Y coordinate value.
    /// </summary>
    public float Y 
    {
        get => y;
        private set => y = value; 
    }

    private float y;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">X coordinate value.</param>
    /// <param name="y">Y coordinate value.</param>
    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Set new coordinate values.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    internal void SetValues(float x, float y)
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
    public static Point operator - (Point first, Point second)
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
}
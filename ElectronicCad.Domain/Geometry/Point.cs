namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Representation of a two demension point on a plane.
/// </summary>
public class Point
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
    /// Set new coordinate values.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    internal void SetValues(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}
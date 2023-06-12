namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Describe RGBA color.
/// </summary>
public class Color
{
    /// <summary>
    /// White color.
    /// </summary>
    public static Color White => new Color(255, 255, 255);
    
    /// <summary>
    /// Transparent color.
    /// </summary>
    public static Color Transparent => new Color(0, 0, 0, 0);

    /// <summary>
    /// Red color.
    /// </summary>
    public byte Red { get; private set; }
    
    /// <summary>
    /// Green color.
    /// </summary>
    public byte Green { get; private set; }

    /// <summary>
    /// Blue color.
    /// </summary>
    public byte Blue { get; private set; }

    /// <summary>
    /// The value of the alpha channel.
    /// </summary>
    public byte Alpha { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Color()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="red">Red color.</param>
    /// <param name="green">Green color.</param>
    /// <param name="blue">Blue color.</param>
    /// <param name="alpha">The value of the alpha channel.</param>
    public Color(byte red, byte green, byte blue, byte alpha = 255)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    /// <summary>
    /// Clone constructor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public Color(Color cloneFrom)
    {
        Red = cloneFrom.Red;
        Blue = cloneFrom.Blue;
        Green = cloneFrom.Green;
        Alpha = cloneFrom.Alpha;
    }
    
    /// <summary>
    /// Copy color from source to target.
    /// </summary>
    /// <param name="target">Target color.</param>
    /// <param name="source">Source color.</param>
    public static void Set(Color target, Color source)
    {
        target.Red = source.Red;
        target.Blue = source.Blue;
        target.Green = source.Green;
        target.Alpha = source.Alpha;
    }
}
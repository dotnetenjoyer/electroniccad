namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Describe RGBA color.
/// </summary>
public struct Color
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
    public byte Red { get; set; }
    
    /// <summary>
    /// Green color.
    /// </summary>
    public byte Green { get; set; }

    /// <summary>
    /// Blue color.
    /// </summary>
    public byte Blue { get; set; }

    /// <summary>
    /// The value of the alpha channel.
    /// </summary>
    public byte Alpha { get; set; }

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
}
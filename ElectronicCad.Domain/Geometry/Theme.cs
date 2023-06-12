namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Presentation theme of geometry object.
/// </summary>
public static class Theme
{
    /// <summary>
    /// Foreground color.
    /// </summary>
    public static Color Foreground { get; }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static Theme()
    {
        Foreground = new Color();
    }

    /// <summary>
    /// Initializes the geometry theme.
    /// </summary>
    /// <param name="foreground">New foreground theme.</param>
    public static void Initialize(Color foreground)
    {
        Color.Set(Foreground, foreground);
    }
}
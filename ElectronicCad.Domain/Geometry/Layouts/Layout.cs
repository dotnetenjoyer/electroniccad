namespace ElectronicCad.Domain.Geometry.Layouts;

/// <summary>
/// Base layout.
/// </summary>
public abstract class Layout 
{
    /// <summary>
    /// Default layout color.
    /// </summary>
    public static Color DefaultColor = new Color(12, 140, 233, 25);

    /// <summary>
    /// Id of the layout.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Color of geometry.
    /// </summary>
    public Color Color { get; init; } = DefaultColor;

    /// <summary>
    /// Indicates whether the layout is visible.
    /// </summary>
    public bool IsVisible { get; init; } = true;
}

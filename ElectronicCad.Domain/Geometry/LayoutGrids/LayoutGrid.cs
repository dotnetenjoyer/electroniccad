namespace ElectronicCad.Domain.Geometry.LayoutGrids;

/// <summary>
/// Base layout grid.
/// </summary>
public abstract class LayoutGrid 
{
    /// <summary>
    /// Default layout grid color.
    /// </summary>
    public static Color DefaultColor = new Color(12, 140, 233, 25);

    /// <summary>
    /// Id of the layout grid.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Color of the grid geometry.
    /// </summary>
    public Color Color { get; init; } = DefaultColor;

    /// <summary>
    /// Indicates whether the layout grid is visible.
    /// </summary>
    public bool IsVisible { get; init; } = true;
}


namespace ElectronicCad.Domain.Geometry.LayoutGrids;

/// <summary>
/// Base layout grid.
/// </summary>
public abstract class LayoutGrid
{
    /// <summary>
    /// Color of the grid geometry.
    /// </summary>
    public Color Color { get; set; } = new Color(255, 0, 0, 25);
}

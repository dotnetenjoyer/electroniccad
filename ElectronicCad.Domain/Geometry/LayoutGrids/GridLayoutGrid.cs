
namespace ElectronicCad.Domain.Geometry.LayoutGrids;

/// <summary>
/// Describe the grid according to the specified values.
/// </summary>
public class GridLayoutGrid : LayoutGrid
{
    /// <summary>
    /// Size of the grid chunk.
    /// </summary>
    public double Size { get; set; }
}
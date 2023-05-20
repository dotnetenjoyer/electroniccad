
namespace ElectronicCad.Domain.Geometry.LayoutGrids;

/// <summary>
/// Describes the grid columns according to the specified values.
/// </summary>
public class ColumnLayoutGrid : LayoutGrid
{
    /// <summary>
    /// Number of columns.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Width of the column.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Offset on the left.
    /// </summary>
    public double Offset { get; set; }
}

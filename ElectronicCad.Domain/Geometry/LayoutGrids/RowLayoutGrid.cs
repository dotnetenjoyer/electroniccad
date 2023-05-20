
namespace ElectronicCad.Domain.Geometry.LayoutGrids;

/// <summary>
/// Describes the grid rows according to the specified values.
/// </summary>
public class RowLayoutGrid : LayoutGrid
{
    /// <summary>
    /// Number of rows.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Height of the row.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Offset on the top.
    /// </summary>
    public double Offset { get; set; }
}

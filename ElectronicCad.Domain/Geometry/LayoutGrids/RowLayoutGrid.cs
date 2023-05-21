
namespace ElectronicCad.Domain.Geometry.LayoutGrids;

/// <summary>
/// Describes the grid rows according to the specified values.
/// </summary>
public class RowLayoutGrid : LayoutGrid
{
    /// <summary>
    /// Number of rows.
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Height of the row.
    /// </summary>
    public double Height { get; init; }

    /// <summary>
    /// Offset on the top.
    /// </summary>
    public double Offset { get; init; }
}

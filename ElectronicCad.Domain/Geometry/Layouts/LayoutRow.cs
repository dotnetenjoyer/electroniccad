
namespace ElectronicCad.Domain.Geometry.Layouts;

/// <summary>
/// Describes the layout rows according to the specified values.
/// </summary>
public class LayoutRow : Layout
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

    /// <summary>
    /// Rows gutter.
    /// </summary>
    public double Gutter { get; init; }
}

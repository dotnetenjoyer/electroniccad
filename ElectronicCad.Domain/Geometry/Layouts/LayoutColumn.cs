
namespace ElectronicCad.Domain.Geometry.Layouts;

/// <summary>
/// Describes the layout columns according to the specified values.
/// </summary>
public class LayoutColumn : Layout
{
    /// <summary>
    /// Number of columns.
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Width of the column.
    /// </summary>
    public double Width { get; init; }

    /// <summary>
    /// Offset on the left.
    /// </summary>
    public double Offset { get; init; }

    /// <summary>
    /// Columns gutter.
    /// </summary>
    public double Gutter { get; init; }
}

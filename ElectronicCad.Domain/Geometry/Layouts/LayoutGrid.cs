
namespace ElectronicCad.Domain.Geometry.Layouts;

/// <summary>
/// Describe the layout grid according to the specified values.
/// </summary>
public class LayoutGrid : Layout
{
    /// <summary>
    /// Size of the grid chunk.
    /// </summary>
    public double Size { get; init; }
}
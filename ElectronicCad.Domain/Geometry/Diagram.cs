namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram.
/// </summary>
public class Diagram
{
    /// <summary>
    /// Diagram id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Layers.
    /// </summary>
    public IEnumerable<Layer> Layers => _layers;

    private List<Layer> _layers = new();

}
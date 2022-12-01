namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram layer.
/// </summary>
public class Layer
{
    /// <summary>
    /// Layer id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Layer name.
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> GeometryObjects => _geometryObjects;

    private List<GeometryObject> _geometryObjects = new();
}
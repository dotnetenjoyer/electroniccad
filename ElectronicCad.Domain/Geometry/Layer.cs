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
    /// Related diagram.
    /// </summary>
    public Diagram Diagram { get; set; }

    /// <summary>
    /// Geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> GeometryObjects => _geometryObjects;

    public List<GeometryObject> _geometryObjects = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Layer name.</param>
    /// <param name="name">Related diagram.</param>
    public Layer(string name, Diagram diagram)
    {
        Id = Guid.NewGuid();
        Name = name;
        Diagram = diagram;
    }

    /// <summary>
    /// Add geometry to layer.
    /// </summary>
    /// <param name="geometry">Geometry object.</param>
    public void AddGeometry(GeometryObject geometry)
    {
        _geometryObjects.Add(geometry);
        Diagram.HandleLayerGeometryAdd(geometry);
    }

    /// <summary>
    /// Remove geometry from layer.
    /// </summary>
    /// <param name="geometry">Geometry object.</param>
    public void RemoveGeometry(GeometryObject geometry)
    {
        _geometryObjects.Remove(geometry);
        Diagram.HandleLayerGeometryRemove(geometry);
    }
}
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
    /// The event fires on geometry add on the layer.
    /// </summary>
    public event EventHandler<GeometryChangedEventArgs> GeometryChanged;

    /// <summary>
    /// Geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> GeometryObjects => _geometryObjects;

    private List<GeometryObject> _geometryObjects = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Layer name.</param>
    public Layer(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    /// <summary>
    /// Add geometry to layer.
    /// </summary>
    /// <param name="geometry">Geometry object.</param>
    public void AddGeometry(GeometryObject geometry)
    {
        _geometryObjects.Add(geometry);

        var eventArgs = new GeometryChangedEventArgs(GeometryChangeType.Add, geometry);
        GeometryChanged?.Invoke(this, eventArgs);
    }

    /// <summary>
    /// Remove geometry from layer.
    /// </summary>
    /// <param name="geometry">Geometry object.</param>
    public void RemoveGeometry(GeometryObject geometry)
    {
        _geometryObjects.Remove(geometry);
        var eventArgs = new GeometryChangedEventArgs(GeometryChangeType.Remove, geometry);
        GeometryChanged?.Invoke(this, eventArgs);
    }
}
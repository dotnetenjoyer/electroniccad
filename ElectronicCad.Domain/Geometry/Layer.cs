using ElectronicCad.Domain.Common;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram layer.
/// </summary>
public class Layer : DomainObservableObject, IGeometryContainer
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
    public Diagram Diagram { get; init; }

    /// <inheritdoc />
    public IEnumerable<GeometryObject> Children => children;

    private readonly List<GeometryObject> children = new();
    
    /// <inheritdoc />
    public IGeometryContainer Parent => Diagram;

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

    /// <inheritdoc />
    public void AddGeometry(GeometryObject geometryObject)
    {
        AddGeometry(new[] { geometryObject });
    }

    /// <inheritdoc />
    public void AddGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometeryObject in geometryObjects)
        {
            geometeryObject.Parent = this;
            children.Add(geometeryObject);
        }
        
        Diagram.RaiseGeometryAdded(geometryObjects);
    }

    /// <inheritdoc />
    public void RemoveGeometry(GeometryObject geometryObject)
    {
        RemoveGeometry(new[] { geometryObject });
    }

    /// <inheritdoc />
    public void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Parent = null;
            children.Remove(geometryObject);
        }

        Diagram.RaiseGeometryRemoved(geometryObjects);
    }
}
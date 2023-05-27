namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Geometry container interface, contains set of members to support containing geometry objects.
/// </summary>
public interface IGeometryContainer
{
    /// <summary>
    /// Children geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> Children { get; }

    /// <summary>
    /// Parent of geometry container.
    /// </summary>
    public IGeometryContainer? Parent { get; }

    /// <summary>
    /// Adds the geometry object to the container.
    /// </summary>
    /// <param name="geometryObjects">Geometry object.</param>
    void AddGeometry(GeometryObject geometryObject);

    /// <summary>
    /// Adds the geometry objects to the container.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects.</param>
    void AddGeometry(IEnumerable<GeometryObject> geometryObjects);

    /// <summary>
    /// Remove the geometry object from the container.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    void RemoveGeometry(GeometryObject geometryObject);

    /// <summary>
    /// Remove the geometry objects from the container.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects.</param>
    void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects);
}

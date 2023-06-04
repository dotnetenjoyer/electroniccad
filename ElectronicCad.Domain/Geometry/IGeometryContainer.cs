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
    /// Adds the geometry objects to the container.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects.</param>
    void AddGeometry(IEnumerable<GeometryObject> geometryObjects);

    /// <summary>
    /// Remove the geometry objects from the container.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects.</param>
    void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects);
}

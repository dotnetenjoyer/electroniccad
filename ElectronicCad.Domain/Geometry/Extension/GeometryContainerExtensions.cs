namespace ElectronicCad.Domain.Geometry.Extensions;

/// <summary>
/// Extension methods for geometry container.
/// </summary>
public static class GeometryContainerExtensions
{
    /// <summary>
    /// Adds a geometry object to the container.
    /// </summary>
    /// <param name="container">Geometry object container.</param>
    /// <param name="geometryObject">Geometry object to add.</param>
    public static void AddGeometry(this IGeometryContainer container, GeometryObject geometryObject)
    {
        container.AddGeometry(new[] { geometryObject });
    }

    /// <summary>
    /// Removes a geometry object from the container.
    /// </summary>
    /// <param name="container">Geometry object container.</param>
    /// <param name="geometryObject">Geometry object to remove.</param>
    public static void RemoveGeometry(this IGeometryContainer container, GeometryObject geometryObject)
    {
        container.RemoveGeometry(new[] { geometryObject });
    }
}
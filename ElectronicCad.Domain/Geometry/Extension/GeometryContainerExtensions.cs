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

    /// <summary>
    /// Indicates if geometry container contains geometry object (recursively). 
    /// </summary>
    /// <param name="container">Geometry object container.</param>
    /// <param name="geometryObject">Target geometry object.</param>
    /// <returns>True if contains geometry object.</returns>
    public static bool Contains(this IGeometryContainer container, GeometryObject geometryObject)
    {
        var containers = container.Children
            .OfType<IGeometryContainer>()
            .ToList();
            
        return container.Children.Contains(geometryObject) 
            || containers.Any(container => container.Contains(geometryObject));
    }
}

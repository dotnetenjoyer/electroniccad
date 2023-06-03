using ElectronicCad.Domain.Geometry.Extensions;

namespace ElectronicCad.Domain.Geometry.Utils;

/// <summary>
/// Contains geometry utils methods.
/// </summary>
public static class GeometryUtils
{
    /// <summary>
    /// Filters geometric objects whose containers contain 
    /// in the collection of target geometric objects
    /// </summary>
    /// <param name="geometryObjects">Geometry objects.</param>
    /// <returns>Filtered collection.</returns>
    public static IEnumerable<GeometryObject> FilterNestingGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        var containers = geometryObjects.OfType<IGeometryContainer>();
        var filteredGeometry = geometryObjects
            .Where(x => containers.All(container => !container.Contains(x)))
            .ToList();

        return filteredGeometry;
    }
}
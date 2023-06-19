namespace ElectronicCad.Domain.Geometry.Extensions;

/// <summary>
/// Geometry objects extension methods.
/// </summary>
public static class GeometryObjectsExtensions
{
    /// <summary>
    /// Creates geometry object clones with clone constructors.
    /// </summary>
    /// <param name="geometryObject">Geometry object to clone.</param>
    /// <returns>Clone of geometry object.</returns>
    public static GeometryObject Clone(this GeometryObject geometryObject)
    {
        var geometryType = geometryObject.GetType();

        var cloneConstructor = geometryType.GetConstructors()
            .First(constructor =>
            {
                var parameters = constructor.GetParameters();
                return parameters.First().ParameterType == geometryType && parameters.Count() == 1;
            });

        var clone = (GeometryObject)cloneConstructor.Invoke(new object[] { geometryObject });
        return clone;
    }
}

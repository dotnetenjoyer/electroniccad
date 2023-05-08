using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Polygone property proxy.
/// </summary>
public class PolygonPropertyProxy : GeometryObjectProxy<Polygon>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="polygone">Polygone.</param>
    public PolygonPropertyProxy(Polygon polygone) : base(polygone)
    {
    }
}
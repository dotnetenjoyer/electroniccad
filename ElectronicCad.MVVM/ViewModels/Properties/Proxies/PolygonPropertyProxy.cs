using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Polygone property proxy.
/// </summary>
public class PolygonPropertyProxy : ContentGeometryObjectProxy<Polygon>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="polygon">Polygon.</param>
    public PolygonPropertyProxy(Polygon polygon) : base(polygon)
    {
    }
}
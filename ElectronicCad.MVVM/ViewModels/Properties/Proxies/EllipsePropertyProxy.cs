using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Ellipse proxy.
/// </summary>
public class EllipsePropertyProxy : GeometryObjectPropertyProxy<Ellipse>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public EllipsePropertyProxy(Ellipse ellipse) : base(ellipse)
    {
    }
}
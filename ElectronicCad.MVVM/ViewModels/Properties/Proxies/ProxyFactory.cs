using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Factory to create proxy object based on source object.
/// </summary>
public static class ProxyFactory
{
    /// <summary>
    /// Creates proxy object.
    /// </summary>
    /// <returns>Proxy object.</returns>
    public static IProxy Create(object sourceObject)
        => sourceObject switch
        {
            Line line => new LinePropertyProxy(line),
            Polygon polygon => new PolygonPropertyProxy(polygon),
            Ellipse ellipse => new EllipsePropertyProxy(ellipse),
            _ => throw new InvalidOperationException()
        };
}
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Workspace;
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
            ProjectDiagram diagram => new ProjectDiagramPropertiesProxy(diagram),
            GeometryGroup group => new GeometryGroupPropertiesProxy(group),
            Line line => new LinePropertyProxy(line),
            Polygon polygon => new PolygonPropertiesProxy(polygon),
            Ellipse ellipse => new EllipsePropertyProxy(ellipse),
            Text text => new TextPropertiesProxy(text),
            Image image => new ImagePropertiesProxy(image),
            _ => throw new InvalidOperationException()
        };
}
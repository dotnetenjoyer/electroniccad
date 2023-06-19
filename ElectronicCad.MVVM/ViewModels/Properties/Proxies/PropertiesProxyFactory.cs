using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Factory to create properties proxy object based on source object.
/// </summary>
public static class PropertiesProxyFactory
{
    /// <summary>
    /// Creates proxy object.
    /// </summary>
    /// <returns>Proxy object.</returns>
    public static IPropertiesProxy Create(object sourceObject)
        => sourceObject switch
        {
            ProjectDiagram diagram => new ProjectDiagramPropertiesProxy(diagram),
            Layer layer => new LayerPropertiesProxy(layer),
            GeometryGroup group => new GeometryGroupPropertiesProxy(group),
            Line line => new LinePropertyProxy(line),
            Polygon polygon => new PolygonPropertiesProxy(polygon),
            Ellipse ellipse => new EllipsePropertyProxy(ellipse),
            Text text => new TextPropertiesProxy(text),
            Image image => new ImagePropertiesProxy(image),
            _ => throw new InvalidOperationException()
        };
}
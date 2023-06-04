using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Geometry object diagram items factory from a geometry objects.
/// </summary>
internal static class GeometryObjectDiagramItemsFactory
{
    private static readonly Dictionary<Type, Func<GeometryObject, GeometryObjectDiagramItem>> factories = new()
    {
        [typeof(Line)] = (line) => new LineDiagramItem((Line)line),
        [typeof(Ellipse)] = (ellipse) => new EllipseDiagramItem((Ellipse)ellipse),
        [typeof(Polygon)] = (polygon) => new PolygonDiagramItem((Polygon)polygon),
        [typeof(Text)] = (text) => new TextDiagramItem((Text)text),
        [typeof(Image)] = (image) => new ImageDiagramItem((Image)image),
        [typeof(GeometryGroup)] = (group) => new GeometryGroupDiagramItem((GeometryGroup)group, ((GeometryGroup)group).Children.Select(x => Create(x))),
    };

    /// <summary>
    /// Create geometry object diagram item instance based on geomtry object.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    /// <returns>Geometry object diagram item.</returns>
    public static GeometryObjectDiagramItem Create(GeometryObject geometryObject)
    {
        return factories[geometryObject.GetType()].Invoke(geometryObject);
    }
}
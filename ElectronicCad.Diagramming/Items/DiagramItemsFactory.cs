using System;
using System.Collections.Generic;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Diagram items factory from a geometry objects.
/// </summary>
internal static class DiagramItemsFactory
{
    private static readonly Dictionary<Type, Func<GeometryObject, DiagramItem>> _factories = new()
    {
        [typeof(Line)] = (GeometryObject line) => new LineDiagramItem((Line)line),
        [typeof(Ellipse)] = (GeometryObject ellipse) => new EllipseDiagramItem((Ellipse)ellipse),
        [typeof(Polygon)] = (GeometryObject polygon) => new PolygonDiagramItem((Polygon)polygon),
        [typeof(Text)] = (GeometryObject text) => new TextDiagramItem((Text)text)
    };

    /// <summary>
    /// Create diagram item instance based on geomtry object.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    /// <returns>Diagram item.</returns>
    public static DiagramItem Create(GeometryObject geometryObject)
    {
        return _factories[geometryObject.GetType()].Invoke(geometryObject);
    }
}
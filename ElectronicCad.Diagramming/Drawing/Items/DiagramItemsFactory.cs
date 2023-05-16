using System;
using System.Collections.Generic;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Diagram items factory from a geometry objects.
/// </summary>
internal static class DiagramItemsFactory
{
    private static readonly Dictionary<Type, Func<GeometryObject, DiagramItem>> factories = new()
    {
        [typeof(Line)] = (line) => new LineDiagramItem((Line)line),
        [typeof(Ellipse)] = (ellipse) => new EllipseDiagramItem((Ellipse)ellipse),
        [typeof(Polygon)] = (polygon) => new PolygonDiagramItem((Polygon)polygon),
        [typeof(Text)] = (text) => new TextDiagramItem((Text)text)
    };

    /// <summary>
    /// Create diagram item instance based on geomtry object.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    /// <returns>Diagram item.</returns>
    public static DiagramItem Create(GeometryObject geometryObject)
    {
        return factories[geometryObject.GetType()].Invoke(geometryObject);
    }
}
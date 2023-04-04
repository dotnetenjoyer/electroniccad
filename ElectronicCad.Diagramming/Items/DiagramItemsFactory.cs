using ElectronicCad.Domain.Geometry;
using System;
using System.Collections.Generic;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Diagram items factory from a geometry objects.
/// </summary>
internal static class DiagramItemsFactory
{
    private static readonly Dictionary<Type, Func<GeometryObject, DiagramItem>> _factories = new()
    {
        [typeof(Line)] = (GeometryObject line) => new LineDiagramItem((Line)line),
        [typeof(Ellipse)] = (GeometryObject ellipse) => new EllipseDiagramItem((Ellipse)ellipse) 
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
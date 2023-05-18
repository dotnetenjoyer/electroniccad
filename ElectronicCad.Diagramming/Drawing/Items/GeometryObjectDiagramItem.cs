﻿using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Base implementation of <see cref="IGeometryObjectDiagramItem"/>
/// </summary>
internal abstract class GeometryObjectDiagramItem : DiagramItem, IGeometryObjectDiagramItem
{
    /// <summary>
    /// Domain geometry object.
    /// </summary>
    public GeometryObject GeometryObject { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="domainObject">Domain object.</param>
    public GeometryObjectDiagramItem(GeometryObject domainObject)
    {
        GeometryObject = domainObject;
    }

    /// <inheritdoc />
    public virtual void UpdateViewState()
    {
        BoundingBox = GeometryObject.BoundingBox.ToSKRect();
        StrokePaint = new SKPaint
        {
            Color = GeometryObject.StrokeColor.ToSKColor(),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = (float)GeometryObject.StrokeWidth,
        };
    }

    /// <inheritdoc />
    public override bool CheckHit(ref SKPoint position)
    {
        return GeometryObject.CheckHit(position.ToDomainPoint());
    }
}

/// <summary>
/// Generic geometry object diagram item.
/// </summary>
/// <typeparam name="TGeometryObject">Type of gemetry object.</typeparam>
internal abstract class GeometryObjectDiagramItem<TGeometryObject> : GeometryObjectDiagramItem where TGeometryObject : GeometryObject
{
    /// <summary>
    /// Certain geometry object.
    /// </summary>
    public TGeometryObject CertainGeometryObject { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryObjectDiagramItem(TGeometryObject geometryObject) : base(geometryObject)
    {
        CertainGeometryObject = geometryObject;
        UpdateViewState();
    }
}
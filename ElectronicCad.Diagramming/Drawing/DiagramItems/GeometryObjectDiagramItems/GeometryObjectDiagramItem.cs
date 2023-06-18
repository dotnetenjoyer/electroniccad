using System;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Drawing.Items;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Diagram item base on domain geometry object.
/// </summary>
internal abstract class GeometryObjectDiagramItem : DiagramItem
{
    /// <summary>
    /// Domain geometry object.
    /// </summary>
    public GeometryObject GeometryObject { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Domain object.</param>
    public GeometryObjectDiagramItem(GeometryObject geometryObject)
    {
        GeometryObject = geometryObject;
        GeometryObject.VersionChanged += HandleGeometryObjectVersionChange;
    }

    private void HandleGeometryObjectVersionChange(object? sender, EventArgs e)
    {
        UpdateViewState();
    }

    /// <inheritdoc />
    public virtual void UpdateViewState()
    {
        BoundingBox = GeometryObject.BoundingBox.ToSKRect();
        IsVisible = GeometryObject.IsVisible;
        IsLock = GeometryObject.IsLock;

        if (StrokePaint != null)
        {
            StrokePaint.Dispose();
        }

        StrokePaint = new SKPaint
        {
            Color = GeometryObject.StrokeColor.ToSKColor(),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = (float)GeometryObject.StrokeWidth,
        };
    }

    /// <inheritdoc />
    public override bool CheckShapeHit(ref SKPoint position)
    {
        return GeometryObject.CheckHit(position.ToDomainPoint());
    }

    /// <inheritdoc />
    protected override void DisposeManagedResources()
    {
        base.DisposeManagedResources();
     
        GeometryObject.VersionChanged -= HandleGeometryObjectVersionChange;
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
    public new TGeometryObject GeometryObject { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryObjectDiagramItem(TGeometryObject geometryObject) : base(geometryObject)
    {
        GeometryObject = geometryObject;
    }
}
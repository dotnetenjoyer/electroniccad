using SkiaSharp;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Polygon diagram node.
/// </summary>
internal class PolygonDiagramItem : ContentGeometryObjectDiagramItem<Polygon>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="polygon">Polygon geometry object.</param>
    public PolygonDiagramItem(Polygon polygon) : base(polygon)
    {
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        base.Draw(drawingContext);
        
        drawingContext.DrawRect(BoundingBox, FillPaint);
        drawingContext.DrawRect(BoundingBox, StrokePaint);
    }
}
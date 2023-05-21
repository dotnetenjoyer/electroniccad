using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

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
        drawingContext.DrawRoundRect(BoundingBox, CertainGeometryObject.CornerRadius, FillPaint);
        drawingContext.DrawRoundRect(BoundingBox, CertainGeometryObject.CornerRadius, StrokePaint);
    }
}
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

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
        UpdateViewState();
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        drawingContext.DrawRoundRect(BoundingBox, GeometryObject.CornerRadius, FillPaint);
        drawingContext.DrawRoundRect(BoundingBox, GeometryObject.CornerRadius, StrokePaint);
    }
}
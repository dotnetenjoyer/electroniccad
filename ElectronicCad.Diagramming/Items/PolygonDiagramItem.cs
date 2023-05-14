using SkiaSharp;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Polygon diagram node.
/// </summary>
internal class PolygonDiagramItem : ContentGeometryObjectDiagramItem
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="polygon">Polygon geometry object.</param>
    public PolygonDiagramItem(Polygon polygon) : base(polygon)
    {
    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);
        
        canvas.DrawRect(BoundingBox, FillPaint);
        canvas.DrawRect(BoundingBox, StrokePaint);
    }
}
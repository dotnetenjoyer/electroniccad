using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Ellipse diagram node.
/// </summary>
internal class EllipseDiagramItem : ContentGeometryObjectDiagramItem<Ellipse>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public EllipseDiagramItem(Ellipse ellipse) : base(ellipse)
    {
        UpdateViewState();
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        var ellipse = GeometryObject;

        drawingContext.DrawEllipse(ellipse.BoundingBox.Center.X, ellipse.BoundingBox.Center.Y,
            ellipse.RadiusX, ellipse.RadiusY, FillPaint);

        drawingContext.DrawEllipse(ellipse.BoundingBox.Center.X, ellipse.BoundingBox.Center.Y,
            ellipse.RadiusX, ellipse.RadiusY, StrokePaint);
    }
}
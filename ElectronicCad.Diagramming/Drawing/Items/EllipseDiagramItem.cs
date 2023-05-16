using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Ellipse diagram node.
/// </summary>
internal class EllipseDiagramItem : ContentGeometryObjectDiagramItem<Ellipse>
{
    /// <summary>
    /// Related ellipse.
    /// </summary>
    internal Ellipse Ellipse => CertainGeometryObject;

    /// <summary>
    /// Constructor.
    /// </summary>
    public EllipseDiagramItem(Ellipse ellipse) : base(ellipse)
    {
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        base.Draw(drawingContext);

        drawingContext.DrawEllipse(Ellipse.BoundingBox.Center.X, Ellipse.BoundingBox.Center.Y,
            Ellipse.RadiusX, Ellipse.RadiusY, FillPaint);

        drawingContext.DrawEllipse(Ellipse.BoundingBox.Center.X, Ellipse.BoundingBox.Center.Y,
            Ellipse.RadiusX, Ellipse.RadiusY, StrokePaint);
    }
}
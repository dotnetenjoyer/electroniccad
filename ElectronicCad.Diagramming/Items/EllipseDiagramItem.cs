using SkiaSharp;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Ellipse diagram node.
/// </summary>
internal class EllipseDiagramItem : ContentGeometryObjectDiagramItem
{
    public Ellipse Ellipse => (Ellipse)GeometryObject;

    /// <summary>
    /// Constructor.
    /// </summary>
    public EllipseDiagramItem(Ellipse ellipse) : base(ellipse)
    {

    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);

        canvas.DrawOval((float)Ellipse.BoundingBox.Center.X, (float)Ellipse.BoundingBox.Center.Y, (float)Ellipse.RadiusX, (float)Ellipse.RadiusY, FillPaint);
        canvas.DrawOval((float)Ellipse.BoundingBox.Center.X, (float)Ellipse.BoundingBox.Center.Y, (float)Ellipse.RadiusX, (float)Ellipse.RadiusY, StrokePaint);
    }
}
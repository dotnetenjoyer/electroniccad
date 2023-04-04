using SkiaSharp;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Ellipse diagram node.
/// </summary>
internal class EllipseDiagramItem : GeometryObjectDiagramItem
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public EllipseDiagramItem(Ellipse ellipse) : base(ellipse)
    {

    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        var center = BoundingBox.GetCenter();
        var xRadius = BoundingBox.Width / 2;
        var yRadius = BoundingBox.Height / 2;

        canvas.DrawOval(center.X, center.Y, xRadius, yRadius, Paints.ForegroundStroke);
        base.Draw(canvas);
    }
}
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Nodes;

/// <summary>
/// Ellipse diagram node.
/// </summary>
public class EllipseDiagramItem : DiagramItem
{
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        var center = Bounds.GetCenter();
        var xRadius = Bounds.Width / 2;
        var yRadius = Bounds.Height / 2;

        canvas.DrawOval(center.X, center.Y, xRadius, yRadius, Paints.ForegroundSolid);
        base.Draw(canvas);
    }
}
using NullSoft.Diagramming.Extensions;
using NullSoft.Diagramming.Utils;
using SkiaSharp;

namespace NullSoft.Diagramming.Nodes;

/// <summary>
/// Ellipse diagram node.
/// </summary>
public class EllipseDiagramNode : DiagramNode
{
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);

        var center = Bounds.GetCenter();
        var xRadius = Bounds.Width / 2;
        var yRadius = Bounds.Height / 2;

        canvas.DrawOval(center.X, center.Y, xRadius, yRadius, PaintUtils.DarkStrokePaint);
    }
}
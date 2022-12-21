using NullSoft.Diagramming.Utils;
using SkiaSharp;

namespace NullSoft.Diagramming.Nodes;

/// <summary>
/// Polygon diagram node.
/// </summary>
public class PolygonDiagramNode : DiagramNode
{
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);
        canvas.DrawRect(Bounds, PaintUtils.DarkStrokePaint);
    }
}
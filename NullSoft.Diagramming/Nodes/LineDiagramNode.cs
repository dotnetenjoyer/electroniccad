using NullSoft.Diagramming.Extensions;
using NullSoft.Diagramming.Utils;
using SkiaSharp;

namespace NullSoft.Diagramming.Nodes;

public class LineDiagramNode : DiagramNode
{
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);
        canvas.DrawLine(Bounds.TopLeft(), Bounds.BottomRight(), PaintUtils.DarkFillPaint);
    }
}
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Nodes;

public class LineDiagramNode : DiagramNode
{
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);
        canvas.DrawLine(Bounds.GetTopLeft(), Bounds.GetBottomRight(), PaintUtils.DarkFillPaint);
    }
}
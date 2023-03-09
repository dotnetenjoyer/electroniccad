using System.Windows;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Nodes;

public class LineDiagramItem : DiagramItem
{
    public override bool CheckHit(Point position)
    {
        return base.CheckHit(position);
    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);
        canvas.DrawLine(Bounds.GetTopLeft(), Bounds.GetBottomRight(), PaintUtils.DarkFillPaint);
    }
}
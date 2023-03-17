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
        canvas.DrawLine(Bounds.GetTopLeft(), Bounds.GetBottomRight(), Paints.ForegroundSolid);
        base.Draw(canvas);
    }
}
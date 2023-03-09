using System.Windows.Media.Animation;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Nodes;

public class SelectionFrameDiagramItem : DiagramItem
{
    internal override bool IsAuxiliary => true;

    public override void Draw(SKCanvas canvas)
    {
        var paint = new SKPaint
        {
            Color = SKColors.Aqua,
            Style = SKPaintStyle.StrokeAndFill
        };

        canvas.DrawRect(Bounds, paint);
    }
}
using SkiaSharp;

namespace NullSoft.Diagramming.Nodes;

public class LineNode : DiagramNode
{
    public override void Draw(SKCanvas canvas)
    {
        var paint = new SKPaint()
        {
            Color = SKColors.Red,
            Style = SKPaintStyle.Fill
        };
        
        canvas.DrawLine(new SKPoint(0, 0), new SKPoint(100, 100), paint);
        canvas.DrawLine(new SKPoint(100, 0), new SKPoint(0, 100), paint);
    }
}

public class Circle : DiagramNode
{
    public override void Draw(SKCanvas canvas)
    {
        var paint = new SKPaint
        {
            Color = SKColors.Blue,
            Style = SKPaintStyle.Fill
        };
     
        canvas.DrawCircle(100, 100, 50, paint);
    }
}
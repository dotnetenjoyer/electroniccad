using System.Windows.Controls;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace ElectronicCad.Desktop.Views.Diagrams;

public partial class DiagramControl : UserControl
{
    public DiagramControl()
    {
        InitializeComponent();
        
        SkElement.PaintSurface += SkElementOnPaintSurface;
    }

    private void SkElementOnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        // var canvas = e.Surface.Canvas;
        //
        // var paint = new SKPaint()
        // {
        //     Color = SKColors.Red,
        //     Style = SKPaintStyle.Fill
        // };
        //
        // canvas.DrawLine(0, 0, 200, 200, paint);
    }
}
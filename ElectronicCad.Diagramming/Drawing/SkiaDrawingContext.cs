using SkiaSharp;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using Topten.RichTextKit;

/// <summary>
/// Drawing context for skia sharp canvas.
/// </summary>
public class SkiaDrawingContext
{
    private readonly SKCanvas canvas;

    private double offsetX, offsetY, scale;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="canvas">Skia canvas.</param>
    public SkiaDrawingContext(SKCanvas canvas)
    {
        this.canvas = canvas;
    }

    /// <summary>
    /// Pre-concatenates the current matrix with the specified translation.
    /// </summary>
    /// <param name="offsetX">The distance to translate in the x-direction</param>
    /// <param name="offsetY">The distance to translate in the y-direction.</param>
    public void Translate(double offsetX, double offsetY)
    {
        this.offsetX += offsetX;
        this.offsetY += offsetY;
    }

    /// <summary>
    /// Scales geometry.
    /// </summary>
    /// <param name="scale">Scale value.</param>
    public void Scale(double scale)
    {
        this.scale += scale;
    }

    /// <summary>
    /// Draw a rectangle on the canvas.
    /// </summary>
    /// <param name="rectangle">Rectangle.</param>
    /// <param name="paint">Paint.</param>
    public void DrawRect(SKRect rectangle, SKPaint paint)
    {
        BeforeDraw();
        canvas.DrawRect(rectangle, paint);
        AfterDraw();
    }

    /// <summary>
    /// Draw a rounded rectangle on the canvas.
    /// </summary>
    /// <param name="rectangle">Rectangle.</param>
    /// <param name="cornerRadius">Corner radius.</param>
    /// <param name="paint">Paint.</param>
    public void DrawRoundRect(SKRect rectangle, double cornerRadius, SKPaint paint)
    {
        BeforeDraw();
        canvas.DrawRoundRect(rectangle, (float)cornerRadius, (float)cornerRadius, paint);
        AfterDraw();
    }

    /// <summary>
    /// Draws a ellipse on the canvas.
    /// </summary>
    /// <param name="centerX">Center coordinate X.</param>
    /// <param name="centerY">Center coordinate Y.</param>
    /// <param name="radiusX">Ellipse radius X.</param>
    /// <param name="radiusY">Ellipse radius Y.</param>
    /// <param name="paint">Paint.</param>
    public void DrawEllipse(double centerX, double centerY , double radiusX, double radiusY, SKPaint paint)
    {
        BeforeDraw();
        canvas.DrawOval((float)centerX, (float)centerY, (float)radiusX, (float)radiusY, paint);
        AfterDraw();
    }

    /// <summary>
    /// Draws a ellipse on the canvas.
    /// </summary>
    /// <param name="center">Center point.</param>
    /// <param name="radius">Ellipse radius.</param>
    /// <param name="paint">Paint.</param>
    public void DrawEllipse(SKPoint center, double radius, SKPaint paint)
    {
        BeforeDraw();
        canvas.DrawOval((float)center.X, (float)center.Y, (float)radius, (float)radius, paint);
        AfterDraw();
    }

    /// <summary>
    /// Draws a line on the canvas.
    /// </summary>
    /// <param name="firstPoint">First line point.</param>
    /// <param name="secondPoint">Second line point.</param>
    /// <param name="paint">Paint.</param>
    public void DrawLine(SKPoint firstPoint, SKPoint secondPoint, SKPaint paint)
    {
        BeforeDraw();
        canvas.DrawLine(firstPoint, secondPoint, paint);
        AfterDraw();
    }

    /// <summary>
    /// Draws a text on the canvas.
    /// </summary>
    /// <param name="richString">Rich string instance.</param>
    /// <param name="x">X coordinate of start drawing point.</param>
    /// <param name="y">Y coordinate of start drawing point.</param>
    public void DrawText(RichString richString, double x, double y)
    {
        BeforeDraw();
        richString.Paint(canvas, new SKPoint((float)x, (float)y));
        AfterDraw();
    }

    /// <summary>
    /// Draws a image on the canvas.
    /// </summary>
    /// <param name="image">Skia image.</param>
    /// <param name="point">Start drawing position.</param>
    public void DrawImage(SKImage image, SKPoint point)
    {
        BeforeDraw();
        canvas.DrawImage(image, point);
        AfterDraw();
    }

    private void BeforeDraw()
    {
        canvas.Scale((float)scale);
        canvas.Translate((float)offsetX, (float)offsetY);
    }

    private void AfterDraw()
    {
        canvas.Translate(-(float)offsetX, -(float)offsetY);
        canvas.Scale(1 / (float)scale);
    }
}
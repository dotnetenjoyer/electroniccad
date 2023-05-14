using SkiaSharp;

/// <summary>
/// Drawing context for skia sharp canvas.
/// </summary>
public class SkiaDrawingContext
{
    private readonly SKCanvas canvas;

    private double deltaX, deltaY;

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
    /// <param name="deltaX">The distance to translate in the x-direction</param>
    /// <param name="deltaY">The distance to translate in the y-direction.</param>
    public void Translate(double deltaX, double deltaY)
    {
        this.deltaX += deltaX;
        this.deltaY += deltaY;
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
    /// <param name="text">Text.</param>
    /// <param name="x">Coordinate X.</param>
    /// <param name="y">Coordinate Y.</param>
    /// <param name="font">Font.</param>
    /// <param name="paint">Paint.</param>
    public void DrawText(string text, double x, double y, SKFont font, SKPaint paint)
    {
        BeforeDraw();
        canvas.DrawText(text, (float)x, (float)y, font, paint);
        AfterDraw();
    }

    private void BeforeDraw()
    {
        canvas.Translate((float)deltaX, (float)deltaY);
    }

    private void AfterDraw()
    {
        canvas.Translate(-(float)deltaX, -(float)deltaY);
    }
}
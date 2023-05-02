using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Base implementation of <see cref="IGeometryObjectDiagramItem"/>
/// </summary>
internal abstract class GeometryObjectDiagramItem : DiagramItem, IGeometryObjectDiagramItem
{
    /// <summary>
    /// Domain geometry object.
    /// </summary>
    public GeometryObject GeometryObject { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="domainObject">Domain object.</param>
    public GeometryObjectDiagramItem(GeometryObject domainObject)
    {
        GeometryObject = domainObject;
        UpdateViewState();
    }

    /// <inheritdoc />
    public virtual void UpdateViewState()
    {
        RecalculateBoundingBox();

        FillPaint = CreateFillPaint();
        StrokePaint = CreateStrokePaint();
    }

    /// <summary>
    /// Recalculate bounding box.
    /// </summary>
    protected void RecalculateBoundingBox()
    {
        var boundingBox = GeometryObject.CalculateBoundingBox();
        BoundingBox = boundingBox.ToSKRect();
    }

    /// <inheritdoc />
    public override bool CheckHit(ref SKPoint position)
    {
        var domainPoint = new Point(position.X, position.Y);
        return GeometryObject.CheckHit(domainPoint);
    }

    private SKPaint CreateFillPaint()
    {
        if (string.IsNullOrEmpty(GeometryObject.Fill))
        {
            return TransparentPaint;
        }

        var fill = ConvertToColor(GeometryObject.Fill);
        var fillColor = new SKColor(fill.red, fill.green, fill.blue);
      
        var paint = new SKPaint
        {
            Color = fillColor,
            Style = SKPaintStyle.StrokeAndFill,
            StrokeWidth = 2,
        };

        return paint;
    }

    private SKPaint CreateStrokePaint()
    {
        if (string.IsNullOrEmpty(GeometryObject.Stroke))
        {
            return TransparentPaint;
        }

        var stroke = ConvertToColor(GeometryObject.Stroke);
        var strokeColor = new SKColor(stroke.red, stroke.green, stroke.blue);

        var paint = new SKPaint
        {
            Color = strokeColor,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
        };

        return paint;
    }

    private (byte red, byte green, byte blue) ConvertToColor(string hexColor)
    {
        hexColor = hexColor.Replace("#", "");

        var red = Convert.ToByte(hexColor.Substring(0, 2), 16);
        var green = Convert.ToByte(hexColor.Substring(2, 2), 16);
        var blue = Convert.ToByte(hexColor.Substring(4, 2), 16);

        return (red, green, blue);
    }
}

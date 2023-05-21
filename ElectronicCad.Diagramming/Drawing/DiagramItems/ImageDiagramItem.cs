using System.IO;
using SkiaSharp;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Diagram item that draws image.
/// </summary>
internal class ImageDiagramItem : ContentGeometryObjectDiagramItem<Image>
{
    private SKImage skiaImage;

    /// <summary>
    /// Constructor,
    /// </summary>
    /// <param name="image">Image.</param>
    public ImageDiagramItem(Image image) : base(image)
    {
        InitializeSkiaImage();
    }

    private void InitializeSkiaImage()
    {
        var content = File.ReadAllBytes(CertainGeometryObject.Reference);
        var bitmap = SKBitmap.Decode(content);
        skiaImage = SKImage.FromBitmap(bitmap);
    }

    /// <inheritdoc />
    public override async void Draw(SkiaDrawingContext context)
    {
        context.DrawImage(skiaImage!, CertainGeometryObject.BoundingBox.Start.ToSKPoint());
    }
}

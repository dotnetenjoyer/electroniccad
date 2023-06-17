using System.IO;
using SkiaSharp;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Diagram item that draws image.
/// </summary>
internal class ImageDiagramItem : ContentGeometryObjectDiagramItem<Image>
{
    private readonly SKBitmap skiaBitmap;

    /// <summary>
    /// Constructor,
    /// </summary>
    /// <param name="image">Image.</param>
    public ImageDiagramItem(Image image) : base(image)
    {
        UpdateViewState();
        skiaBitmap = SKBitmap.Decode(File.ReadAllBytes(GeometryObject.Reference));
    }

    /// <inheritdoc />
    public override async void Draw(SkiaDrawingContext context)
    {
        context.DrawBitmap(skiaBitmap, BoundingBox);
    }
}

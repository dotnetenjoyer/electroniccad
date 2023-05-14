using SkiaSharp;
using ElectronicCad.Diagramming.Items;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Utils;

namespace ElectronicCad.Diagramming;

/// <summary>
/// The diagram item to draw text.
/// </summary>
internal class TextDiagramItem : GeometryObjectDiagramItem<Text>
{
    /// <summary>
    /// Text geometry object..
    /// </summary>
    internal Text Text => CertainGeometryObject;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="text">Text.</param>
    public TextDiagramItem(Text text) : base(text)
    {
    }

    /// <inhertidoc />
    public override void Draw(SkiaDrawingContext context)
    {
        base.Draw(context);

        var font = new SKFont(SKTypeface.Default, size: 16);
        context.DrawText(Text.Content, Text.BoundingBox.Center.X, Text.BoundingBox.Center.Y, font, Paints.ForegroundStroke);
    }
}
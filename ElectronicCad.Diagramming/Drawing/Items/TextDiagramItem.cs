using Topten.RichTextKit;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Drawing.Items;

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

        var richString = new RichString()
            .Alignment(TextAlignment.Center)
            .FontFamily(Text.FontFamily)
            .FontSize((float)Text.FontSize)
            .FontWeight((int)Text.FontWeight)
            .TextColor(Text.FillColor.ToSKColor())
            .Add(Text.Content);

        richString.MaxWidth = (float)Text.BoundingBox.Width;
        richString.MaxHeight = (float)Text.BoundingBox.Height;

        context.DrawText(richString, Text.BoundingBox.Start.X, Text.BoundingBox.Start.Y);
    }
}
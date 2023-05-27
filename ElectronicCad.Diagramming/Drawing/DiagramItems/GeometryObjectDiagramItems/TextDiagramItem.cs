using Topten.RichTextKit;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// The diagram item to draw text.
/// </summary>
internal class TextDiagramItem : GeometryObjectDiagramItem<Text>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="text">Text.</param>
    public TextDiagramItem(Text text) : base(text)
    {
        UpdateViewState();
    }

    /// <inhertidoc />
    public override void Draw(SkiaDrawingContext context)
    {
        var text = GeometryObject;

        var richString = new RichString()
            .Alignment(TextAlignment.Center)
            .FontFamily(text.FontFamily)
            .FontSize((float)text.FontSize)
            .FontWeight((int)text.FontWeight)
            .TextColor(text.FillColor.ToSKColor())
            .Add(text.Content);

        richString.MaxWidth = (float)text.BoundingBox.Width;
        richString.MaxHeight = (float)text.BoundingBox.Height;

        context.DrawText(richString, text.BoundingBox.Start.X, text.BoundingBox.Start.Y);
    }
}
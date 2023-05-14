using SkiaSharp;
using ElectronicCad.Diagramming.Items;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using System.Drawing;

namespace ElectronicCad.Diagramming;

internal class TextDiagramItem : GeometryObjectDiagramItem
{
    private readonly Text text;

    private static readonly SKFont font = new SKFont(SKTypeface.Default, size: 16);

    public TextDiagramItem(Text text) : base(text)
    {
        this.text = text;
    }

    /// <inhertidoc />
    //public override void Draw(SKCanvas canvas)
    //{
    //    base.Draw(canvas);
    //    var leftCenter = BoundingBox.GetLeftCenter();
    //    canvas.DrawText(text.Content, leftCenter.X, leftCenter.Y, font, Paints.ForegroundStroke);
    //}
}
using SkiaSharp;
using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the layout column visual presentation.
/// </summary>
internal class LayoutColumnDiagramItem : LayoutDiagramItem<LayoutColumn>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutGrid">Column layout grid.</param>
    public LayoutColumnDiagramItem(Diagram diagram, LayoutColumn layoutGrid) 
        : base(diagram, layoutGrid)
    {
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        if (!layout.IsVisible)
        {
            return;
        }

        using var paint = CreatePaint();
        //var gutterWidth = CalculateGutterWidth();

        for (int i = 0; i < layout.Count; i++)
        {
            var left = layout.Offset + (layout.Width + layout.Gutter) * i;
            var rigth = left + layout.Width;
            var top = 0;
            var bottom = diagram.GeometryDiagram.Size.Height;
            var rect = new SKRect((float)left, top, (float)rigth, (float)bottom);
            drawingContext.DrawRect(rect, paint);
        }
    }

    //private float CalculateGutterWidth()
    //{
    //    var reservedWidth = layout.Offset + layout.Count * layout.Width;
    //    var remainingWidth = diagram.GeometryDiagram.Size.Width - reservedWidth;
    //    var gutterWidth = remainingWidth / layout.Count;
    //    return (float)gutterWidth;
    //}
}

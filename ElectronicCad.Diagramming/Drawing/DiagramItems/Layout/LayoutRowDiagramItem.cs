using SkiaSharp;
using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the layout row visual presentation.
/// </summary>
internal class LayoutRowDiagramItem : LayoutDiagramItem<LayoutRow>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutRow">Row layout grid.</param>
    public LayoutRowDiagramItem(Diagram diagram, LayoutRow layoutRow)
        : base(diagram, layoutRow)
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
        //var gutterHeight = CalculateGutterHeight();

        for (int i = 0; i < layout.Count; i++)
        {
            var left = 0;
            var rigth = diagram.GeometryDiagram.Size.Width;
            var top = layout.Offset + (layout.Height + layout.Gutter) * i;
            var bottom = top + layout.Height;
            var rect = new SKRect(left, (float)top, (float)rigth, (float)bottom);
            drawingContext.DrawRect(rect, paint);
        }
    }

    //private float CalculateGutterHeight()
    //{
    //    var reservedHeight = layout.Offset + layout.Count * layout.Height;
    //    var remainingHeight = diagram.GeometryDiagram.Size.Height - reservedHeight;
    //    var gutterHeight = remainingHeight / layout.Count;
    //    return (float)gutterHeight;
    //}
}


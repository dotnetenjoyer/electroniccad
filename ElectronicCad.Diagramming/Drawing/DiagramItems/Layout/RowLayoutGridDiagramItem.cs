using SkiaSharp;
using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the row layout grid visual presentation.
/// </summary>
internal class RowLayoutGridDiagramItem : LayoutGridDiagramItem<RowLayoutGrid>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutGrid">Row layout grid.</param>
    public RowLayoutGridDiagramItem(Diagram diagram, RowLayoutGrid layoutGrid)
        : base(diagram, layoutGrid)
    {
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        if (!layoutGrid.IsVisible)
        {
            return;
        }

        using var paint = CreatePaint();
        var gutterHeight = CalculateGutterHeight();

        for (int i = 0; i < layoutGrid.Count; i++)
        {
            var left = 0;
            var rigth = diagram.GeometryDiagram.Width;
            var top = layoutGrid.Offset + (layoutGrid.Height + gutterHeight) * i;
            var bottom = top + layoutGrid.Height;
            var rect = new SKRect(left, (float)top, rigth, (float)bottom);
            drawingContext.DrawRect(rect, paint);
        }
    }

    private float CalculateGutterHeight()
    {
        var reservedHeight = layoutGrid.Offset + layoutGrid.Count * layoutGrid.Height;
        var remainingHeight = diagram.GeometryDiagram.Height - reservedHeight;
        var gutterHeight = remainingHeight / layoutGrid.Count;
        return (float)gutterHeight;
    }
}


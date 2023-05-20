using SkiaSharp;
using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

internal class ColumnLayoutGridDiagramItem : LayoutGridDiagramItem<ColumnLayoutGrid>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutGrid">Column layout grid.</param>
    public ColumnLayoutGridDiagramItem(Diagram diagram, ColumnLayoutGrid layoutGrid) 
        : base(diagram, layoutGrid)
    {
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        using var paint = CreatePaint();
        var gutterWidth = CalculateGutterWidth();

        for (int i = 0; i < layoutGrid.Count; i++)
        {
            var left = layoutGrid.Offset + (layoutGrid.Width + gutterWidth) * i;
            var rigth = left + layoutGrid.Width;
            var top = 0;
            var bottom = diagram.GeometryDiagram.Height;
            var rect = new SKRect((float)left, top, (float)rigth, bottom);
            drawingContext.DrawRect(rect, paint);
        }
    }

    private float CalculateGutterWidth()
    {
        var reservedWidth = layoutGrid.Offset + layoutGrid.Count * layoutGrid.Width;
        var remainingWidth = diagram.GeometryDiagram.Width - reservedWidth;
        var gutterWidth = remainingWidth / layoutGrid.Count;
        return (float)gutterWidth;
    }
}

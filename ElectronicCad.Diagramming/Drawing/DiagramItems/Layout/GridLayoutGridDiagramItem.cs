using SkiaSharp;
using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the row layout grid visual presentation.
/// </summary>
internal class GridLayoutGridDiagramItem : LayoutGridDiagramItem<GridLayoutGrid>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutGrid">Row layout grid.</param>
    public GridLayoutGridDiagramItem(Diagram diagram, GridLayoutGrid layoutGrid)
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
        var lineThickness = 2;

        var columnCount = (int)(diagram.GeometryDiagram.Size.Width / layoutGrid.Size);
        var rowCount = (int)(diagram.GeometryDiagram.Size.Height / layoutGrid.Size);

        for (int i = 1; i <= columnCount; i++) 
        {
            var left = (layoutGrid.Size * i) - (lineThickness / 2);
            var rigth = left + lineThickness;
            var top = 0;
            var bottom = diagram.GeometryDiagram.Size.Height;
            var rect = new SKRect((float)left, (float)top, (float)rigth, (float)bottom);
            drawingContext.DrawRect(rect, paint);
        }

        for (int i = 1; i <= rowCount; i++)
        {
            var left = 0;
            var rigth = diagram.GeometryDiagram.Size.Width;
            var top = (layoutGrid.Size * i) - (lineThickness / 2);
            var bottom = top + lineThickness;
            var rect = new SKRect((float)left, (float)top, (float)rigth, (float)bottom);
            drawingContext.DrawRect(rect, paint);
        }
    }
}


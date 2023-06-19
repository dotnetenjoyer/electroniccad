using SkiaSharp;
using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the layout grid visual presentation.
/// </summary>
internal class LayoutGridDiagramItem : LayoutDiagramItem<LayoutGrid>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutGrid">Row layout grid.</param>
    public LayoutGridDiagramItem(Diagram diagram, LayoutGrid layoutGrid)
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
        var lineThickness = 2;

        var columnCount = (int)(diagram.GeometryDiagram.Size.Width / layout.Size);
        var rowCount = (int)(diagram.GeometryDiagram.Size.Height / layout.Size);

        for (int i = 1; i <= columnCount; i++) 
        {
            var left = (layout.Size * i) - (lineThickness / 2);
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
            var top = (layout.Size * i) - (lineThickness / 2);
            var bottom = top + lineThickness;
            var rect = new SKRect((float)left, (float)top, (float)rigth, (float)bottom);
            drawingContext.DrawRect(rect, paint);
        }
    }
}


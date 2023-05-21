using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.Items;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the layout grid visual presentation.
/// </summary>
internal abstract class LayoutGridDiagramItem<TLayoutGrid> : DiagramItem where TLayoutGrid : LayoutGrid
{
    protected readonly Diagram diagram;
    protected readonly TLayoutGrid layoutGrid;

    /// <inheritdoc />
    public override bool IsAuxiliary => false;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layoutGrid">Domain layout grid.</param>
    public LayoutGridDiagramItem(Diagram diagram, TLayoutGrid layoutGrid)
    {
        this.diagram = diagram;
        this.layoutGrid = layoutGrid;
    }

    /// <summary>
    /// Creates skia paint to layout grid.
    /// </summary>
    /// <returns>Skia paint.</returns>
    protected virtual SKPaint CreatePaint()
    {
        var paint = new SKPaint
        {
            Color = layoutGrid.Color.ToSKColor(),
            Style = SKPaintStyle.StrokeAndFill
        };

        return paint;
    }
}

using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.Items;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;

/// <summary>
/// Represent the layout visual presentation.
/// </summary>
internal abstract class LayoutDiagramItem<TLayoutGrid> : DiagramItem where TLayoutGrid : Domain.Geometry.Layouts.Layout
{
    protected readonly Diagram diagram;
    protected readonly TLayoutGrid layout;

    /// <inheritdoc />
    public override bool IsAuxiliary => false;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="layout">Domain layout</param>
    public LayoutDiagramItem(Diagram diagram, TLayoutGrid layout)
    {
        this.diagram = diagram;
        this.layout = layout;
    }

    /// <summary>
    /// Creates skia paint to layout.
    /// </summary>
    /// <returns>Skia paint.</returns>
    protected virtual SKPaint CreatePaint()
    {
        var paint = new SKPaint
        {
            Color = layout.Color.ToSKColor(),
            Style = SKPaintStyle.StrokeAndFill
        };

        return paint;
    }
}

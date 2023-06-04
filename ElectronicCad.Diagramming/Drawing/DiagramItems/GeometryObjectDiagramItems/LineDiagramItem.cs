using SkiaSharp.Views.Desktop;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Line diagram item that bind with domain geometry object.
/// </summary>
internal class LineDiagramItem : GeometryObjectDiagramItem<Line>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public LineDiagramItem(Line line) : base(line)
    {
        UpdateViewState();
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext context)
    {
        var line = GeometryObject;
        var firstPoint = line.FirstPoint.ToSKPoint();
        var secondPoint = line.SecondPoint.ToSKPoint();
        context.DrawLine(firstPoint, secondPoint, StrokePaint);
    }
}
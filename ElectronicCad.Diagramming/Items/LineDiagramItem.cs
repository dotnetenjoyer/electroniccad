using SkiaSharp.Views.Desktop;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Items;

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
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext context)
    {
        base.Draw(context);

        var firstPoint = CertainGeometryObject.ControlPoints[Line.FirstPointIndex].ToSKPoint();
        var secondPoint = CertainGeometryObject.ControlPoints[Line.SecondPointIndex].ToSKPoint();
        context.DrawLine(firstPoint, secondPoint, StrokePaint);
    }
}
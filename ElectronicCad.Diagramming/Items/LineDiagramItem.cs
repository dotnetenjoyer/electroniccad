using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Line diagram item that bind with domain geometry object.
/// </summary>
internal class LineDiagramItem : GeometryObjectDiagramItem
{
    /// <summary>
    /// Constructor
    /// </summary>
    public LineDiagramItem(Line line) : base(line)
    {
    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        base.Draw(canvas);

        var firstPoint = GeometryObject.ControlPoints[Line.FirstPointIndex].ToSKPoint();
        var secondPoint = GeometryObject.ControlPoints[Line.SecondPointIndex].ToSKPoint();
        canvas.DrawLine(firstPoint, secondPoint, Paints.ForegroundSolid);
    }
}
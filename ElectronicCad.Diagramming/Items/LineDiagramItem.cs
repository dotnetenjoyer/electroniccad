using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Utils;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Line diagram item that bind with domain geometry object.
/// </summary>
internal class LineDiagramItem : GeometryDiagramItem<Line>
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

        var firstPoint = DomainObject.ControlPoints[0].ToSKPoint();
        var secondPoint = DomainObject.ControlPoints[1].ToSKPoint();

        canvas.DrawLine(firstPoint, secondPoint, Paints.ForegroundSolid);
    }
}
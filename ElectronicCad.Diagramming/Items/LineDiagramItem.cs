using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Diagramming.Extensions;
using SkiaSharp;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Nodes;

public class LineDiagramItem : DiagramItem<Line>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public LineDiagramItem(Line line) : base(line)
    {
        var a = line.CalculateBoundingBox();
    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        canvas.DrawLine(Bounds.GetTopLeft(), Bounds.GetBottomRight(), Paints.ForegroundSolid);
        base.Draw(canvas);
    }
}
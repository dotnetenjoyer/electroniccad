using ElectronicCad.Diagramming.Utils;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Nodes;

/// <summary>
/// Polygon diagram node.
/// </summary>
public class PolygonDiagramItem : DiagramItem
{
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        canvas.DrawRect(Bounds, Paints.ForegroundSolid);
        base.Draw(canvas);
    }
}
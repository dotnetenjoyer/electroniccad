using SkiaSharp;

namespace NullSoft.Diagramming.Nodes;

/// <summary>
/// The class that represent diagram node.
/// </summary>
public abstract class DiagramNode
{
    public abstract void Draw(SKCanvas canvas);
}
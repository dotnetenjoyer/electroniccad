using System.Windows;
using NullSoft.Diagramming.Utils;
using SkiaSharp;

namespace NullSoft.Diagramming.Nodes;

/// <summary>
/// The class that represent diagram node.
/// </summary>
public abstract class DiagramNode
{
    /// <summary>
    /// Layer.
    /// </summary>
    public Layer? Layer { get; set; }
    
    /// <summary>
    /// Node z index.
    /// </summary>
    public int ZIndex { get; set; }
    
    /// <summary>
    /// Diagram node bounds.
    /// </summary>
    public SKRect Bounds { get; set; }

    /// <summary>
    /// The method containing redirection logic.
    /// </summary>
    /// <param name="canvas">Skia canvas.</param>
    public virtual void Draw(SKCanvas canvas)
    {
        // canvas.DrawRect(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height, PaintUtils.RedStrokePaint);
    }
}
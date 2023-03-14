using System.Windows;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using SkiaSharp;
using SkiaSharp.Views.WPF;

namespace ElectronicCad.Diagramming.Nodes;

/// <summary>
/// The class that represent diagram item with visual presentation.
/// </summary>
public abstract class DiagramItem
{
    /// <summary>
    /// Indicate whether item is auxiliary. 
    /// </summary>
    internal virtual bool IsAuxiliary => false;
    
    /// <summary>
    /// Diagram node bounds.
    /// </summary>
    public SKRect Bounds { get; set; }
    
    /// <summary>
    /// Layer.
    /// </summary>
    public Layer? Layer { get; set; }
    
    /// <summary>
    /// Z index.
    /// </summary>
    public int ZIndex { get; set; }

    /// <summary>
    /// The method containing redirection logic.
    /// </summary>
    /// <param name="canvas">Skia canvas.</param>
    public virtual void Draw(SKCanvas canvas)   
    {
        // Debug rectangle.
        // canvas.DrawRect(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height, PaintUtils.RedStrokePaint);
    }

    /// <summary>
    /// Check point hitting with diagram item.
    /// </summary>
    /// <param name="position">Point position.</param>
    /// <returns>Whether hit or not.</returns>
    public virtual bool CheckHit(Point position)
    {
        return Bounds.ContainsTest(position.ToSKPoint());
    }
}
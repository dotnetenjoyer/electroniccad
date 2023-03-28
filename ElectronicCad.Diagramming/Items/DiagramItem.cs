using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using ElectronicCad.Diagramming.Utils;

namespace ElectronicCad.Diagramming.Nodes;

/// <summary>
/// The class that represent diagram item with visual presentation.
/// </summary>
internal abstract class DiagramItem
{
    /// <summary>
    /// Indicate whether item is auxiliary. 
    /// </summary>
    internal virtual bool IsAuxiliary => false;

    /// <summary>
    /// Indicates whether it is visible.
    /// </summary>
    public virtual bool IsVisible { get; set; } = true;
    
    /// <summary>
    /// Diagram item bounding box.
    /// </summary>
    public SKRect BoundingBox { get; set; }
    
    /// <summary>
    /// Layer.
    /// </summary>
    public Layer? Layer { get; set; }
    
    /// <summary>
    /// Z index.
    /// </summary>
    public int ZIndex { get; set; }

    /// <summary>
    /// Draws itself on canvas.
    /// </summary>
    /// <param name="canvas">Skia canvas.</param>
    public virtual void Draw(SKCanvas canvas)   
    {
        // Debug rectangle.
        canvas.DrawRect(BoundingBox, Paints.DebugPaint);
    }
    
    /// <summary>
    /// Check point hitting.
    /// </summary>
    /// <param name="position">Point position.</param>
    /// <returns>Whether hit or not.</returns>
    public virtual bool CheckHit(Point position)
    {
        var skPoint = position.ToSKPoint();
        return BoundingBox.Contains(skPoint);
    }
}
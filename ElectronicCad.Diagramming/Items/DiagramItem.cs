using SkiaSharp;
using System;

namespace ElectronicCad.Diagramming.Items;

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
    /// 
    /// </summary>
    public bool IsFocused { get; private set; }

    /// <summary>
    /// Draws itself on canvas.
    /// </summary>
    /// <param name="canvas">Skia canvas.</param>
    public virtual void Draw(SKCanvas canvas)   
    {
        // Debug rectangle.
        //canvas.DrawRect(BoundingBox, Paints.DebugPaint);
    }
    
    /// <summary>
    /// Check point hitting.
    /// </summary>
    /// <param name="position">Point position.</param>
    /// <returns>Whether hit or not.</returns>
    public virtual bool CheckHit(ref SKPoint point)
    {
        return BoundingBox.Contains(point);
    }

    /// <summary>
    /// Handling left mouse up over a diagram element
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    internal void HandleMouseUp(MouseParameters mouse)
    {
    }

    /// <summary>
    /// Handling left mouse down over a diagram element
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    public virtual void HandleMouseDown(MouseParameters mouse)
    {
    
    }

    /// <summary>
    /// Handling mouse movements over a diagram element.
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    public virtual void HandleMouseMove(MovingMouseParameters mouse)
    {
    
    }
}
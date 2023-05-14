using SkiaSharp;
using System;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// The class that represent diagram item with visual presentation.
/// </summary>
internal abstract class DiagramItem
{
    /// <summary>
    /// Transparent skia paint.
    /// </summary>
    public readonly static SKPaint TransparentPaint = new SKPaint
    {
        Color = SKColors.Transparent
    };

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
    /// Diagram item stroke paint.
    /// </summary>
    public SKPaint StrokePaint { get; set; }

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
    /// Raises when clicked mouse button up on diagram item.
    /// </summary>
    public event EventHandler<MouseParameters> MouseUp;

    /// <summary>
    /// Raises when click on diagram item.
    /// </summary>
    public event EventHandler<MouseParameters> MouseDown;   

    /// <summary>
    /// Raises when mouse move on diagram item.
    /// </summary>
    public event EventHandler<MovingMouseParameters> MouseMove;   

    /// <summary>
    /// Checks if mouse ups on the diagram item.
    /// If so, invokes the apropriate event.
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    /// <returns>True if ups on the diagram item.</returns>
    public virtual bool CheckMouseUp(MouseParameters mouse)
    {
        var position = mouse.Position;
        if (CheckHit(ref position))
        {
            MouseUp?.Invoke(this, mouse);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if mouse downs on the diagram item.
    /// If so, invokes the apropriate event.
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    /// <returns>True if downs on the diagram item.</returns>
    public virtual bool CheckMouseDown(MouseParameters mouse)
    {
        var position = mouse.Position;
        if (CheckHit(ref position))
        {
            MouseDown?.Invoke(this, mouse);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if mouse moves on the diagram item.
    /// If so, invokes the apropriate event.
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    /// <returns>True if moves on the diagram item.</returns>
    public virtual bool CheckMouseMove(MovingMouseParameters mouse)
    {
        var position = mouse.Position;
        if (CheckHit(ref position))
        {
            MouseMove?.Invoke(this, mouse);
            return true;
        }

        return false;
    }

    /// <summary>
    /// HACK: Raise mouse move event directly,
    /// can cite to wrong relative positions calculations,
    /// because geometry diagram item bounding box may not have time to update.
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    public void RaiseMouseMove(MovingMouseParameters mouse)
    {
        MouseMove?.Invoke(this, mouse);
    }
}
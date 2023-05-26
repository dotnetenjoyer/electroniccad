using System;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// The class that represent diagram item with visual presentation.
/// </summary>
internal abstract class DiagramItem : IDisposable
{
    /// <summary>
    /// Indicate whether item is auxiliary. 
    /// </summary>
    public virtual bool IsAuxiliary => false;

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
    /// Stroke geometry paint.
    /// </summary>
    public SKPaint StrokePaint { get; protected set; }

    /// <summary>
    /// Draws itself.
    /// </summary>
    /// <param name="drawingcontext">Skia drawing context.</param>
    public abstract void Draw(SkiaDrawingContext drawingContext);

    /// <summary>
    /// Check point hitting to the geometry shape.
    /// </summary>
    /// <param name="position">Hit point.</param>
    /// <returns>True if hits to the geometry shape.</returns>
    public virtual bool CheckShapeHit(ref SKPoint point)
    {
        return CheckBoundingBoxHit(ref point);
    }

    /// <summary>
    /// Check point hitting to the bounding box.
    /// </summary>
    /// <param name="position">Hit point.</param>
    /// <returns>True if hit to the bounding box.</returns>
    public virtual bool CheckBoundingBoxHit(ref SKPoint point)
    {
        return BoundingBox.Contains(point);
    }

    #region Mouse events 

    /// <summary>
    /// Raises when clicked mouse button up on diagram item.
    /// </summary>
    public event EventHandler<MouseParameters>? MouseUp;

    /// <summary>
    /// Raises when click on diagram item.
    /// </summary>
    public event EventHandler<MouseParameters>? MouseDown;

    /// <summary>
    /// Raises when mouse move on diagram item.
    /// </summary>
    public event EventHandler<MovingMouseParameters>? MouseMove;

    /// <summary>
    /// Handle the diagram mouse ups, if ups on the current item, invokes the apropriate event.
    /// </summary>
    /// <param name="mouseParameters">Mouse parameters.</param>
    /// <returns>True if ups on the current item.</returns>
    public virtual bool HandleDiagramMouseUp(MouseParameters mouseParameters)
    {
        var position = mouseParameters.Position;
        
        if (!CheckBoundingBoxHit(ref position))
        {
            return false;
        }
        else if (CheckShapeHit(ref position))
        {
            MouseUp?.Invoke(this, mouseParameters);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Handle the diagram mouse downs, if downs on the current item, invokes the apropriate event.
    /// </summary>
    /// <param name="mouseParameters">Mouse parameters.</param>
    /// <returns>True if downs on the diagram item.</returns>
    public virtual bool HandleDiagramMouseDown(MouseParameters mouseParameters)
    {
        var position = mouseParameters.Position;

        if (!CheckBoundingBoxHit(ref position))
        {
            return false;
        }
        else if (CheckShapeHit(ref position))
        {
            MouseDown?.Invoke(this, mouseParameters);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Handle the diagram mouse moves, if moves on the current item, invokes the apropriate event.
    /// </summary>
    /// <param name="mouse">Mouse parameters.</param>
    /// <returns>True if moves on the diagram item.</returns>
    public virtual bool HandleDiagramMouseMove(MovingMouseParameters mouse)
    {
        var position = mouse.Position;

        if (!CheckBoundingBoxHit(ref position))
        {
            return false;
        }
        else if (CheckShapeHit(ref position))
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

    #endregion

    /// <inheritdoc />
    public void Dispose()
    {
        DisposeManagedResources();
    }
    
    /// <summary>
    /// Disposes managed resources.
    /// </summary>
    protected virtual void DisposeManagedResources()
    {
        if (StrokePaint != null)
        {
            StrokePaint.Dispose();
        }
    }
}
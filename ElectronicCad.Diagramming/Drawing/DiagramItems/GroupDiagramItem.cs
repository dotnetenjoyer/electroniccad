using System;
using System.Linq;
using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Diagramming.Utils;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Group of diagram items.
/// </summary>
internal class GroupDiagramItem : DiagramItem
{
    /// <summary>
    /// Children.
    /// </summary>
    public IEnumerable<DiagramItem> Children { get; protected set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GroupDiagramItem()
    {
        Children = Array.Empty<DiagramItem>();
        BoundingBox = SKRect.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="children">Children.</param>
    public GroupDiagramItem(IEnumerable<DiagramItem> children)
    {
        Children = children;
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Recalcualtes group bounds based on the children bounds.
    /// </summary>
    public void RecalculateBoundingBox()
    {
        BoundingBox = SkiaRectangleUtils
            .CalculateScribedRectangle(Children.Select(x => x.BoundingBox));
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        foreach (var child in Children)
        {
            child.Draw(context);
        }
    }

    /// <inheritdoc />
    public override bool CheckShapeHit(ref SKPoint point)
    {
        foreach (var child in Children)
        {
            if (child.CheckShapeHit(ref point))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseDown(MouseParameters mouse)
    {
        foreach (var child in Children)
        {
            if (child.HandleDiagramMouseDown(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseUp(MouseParameters mouse)
    {
        foreach (var child in Children)
        {
            if (child.HandleDiagramMouseUp(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseMove(MovingMouseParameters mouse)
    {
        foreach (var child in Children)
        {
            if (child.HandleDiagramMouseMove(mouse))
            {
                return true;
            }
        }

        return false;
    }
}
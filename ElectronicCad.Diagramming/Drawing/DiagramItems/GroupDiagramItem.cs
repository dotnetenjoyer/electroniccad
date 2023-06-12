using System.Linq;
using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Diagramming.Utils;
using System.Configuration.Internal;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Group of the diagram items.
/// </summary>
internal class GroupDiagramItem : DiagramItem, IDiagramItemContainer
{
    /// <inheritdoc />
    public IEnumerable<DiagramItem> Children => children;

    private readonly List<DiagramItem> children = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public GroupDiagramItem()
    {
        BoundingBox = SKRect.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="children">Children.</param>
    public GroupDiagramItem(IEnumerable<DiagramItem> children)
    {
        this.children.AddRange(children);
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Recalculates the group bounding box based on the children bounds.
    /// </summary>
    public virtual void RecalculateBoundingBox()
    {
        BoundingBox = SkiaRectangleUtils
            .CalculateScribedRectangle(Children.Select(x => x.BoundingBox));
    }

    /// <inheritdoc />
    public void AddChildren(IEnumerable<DiagramItem> children)
    {
        foreach (var child in children)
        {
            this.children.Add(child);
            child.Group = this;
        }

        RecalculateBoundingBox();
    }

    /// <inheritdoc />
    public void RemoveChildren(IEnumerable<DiagramItem> children)
    {
        foreach (var child in children)
        {
            var isDeleteSuccessed = this.children.Remove(child);
            if (isDeleteSuccessed)
            {
                child.Group = null;
            }
        }

        RecalculateBoundingBox();
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        if (!IsVisible)
        {
            return;
        }

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
    public override bool CheckMouseDown(MouseParameters mouse, out DiagramItem? interactionItem)
    {
        foreach (var child in Children)
        {
            if (child.CheckMouseDown(mouse, out var childInteractionItem))
            {
                interactionItem = childInteractionItem;
                return true;
            }
        }

        interactionItem = null;
        return false;
    }

    /// <inheritdoc />
    public override bool CheckMouseUp(MouseParameters mouse, out DiagramItem? interactionItem)
    {
        foreach (var child in Children)
        {
            if (child.CheckMouseUp(mouse, out var childInteractionItem))
            {
                interactionItem = childInteractionItem;
                return true;
            }
        }

        interactionItem = null;
        return false;
    }

    /// <inheritdoc />
    public override bool CheckMouseMove(MovingMouseParameters mouse, out DiagramItem? interactionItem)
    {
        foreach (var child in Children)
        {
            if (child.CheckMouseMove(mouse, out DiagramItem? childInteractionItem))
            {
                interactionItem = childInteractionItem;
                return true;
            }
        }

        interactionItem = null;
        return false;
    }
}

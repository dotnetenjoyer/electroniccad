using System.Linq;
using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Diagramming.Utils;

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

using SkiaSharp;
using System.Collections.Generic;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Group of diagram items.
/// </summary>
internal class GroupDiagramItem : DiagramItem
{
    /// <summary>
    /// Collection of grouped items.
    /// </summary>
    protected readonly ICollection<DiagramItem> GroupedItems = new List<DiagramItem>();

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        foreach (DiagramItem item in GroupedItems)
        {
            item.Draw(context);
        }
    }

    /// <inheritdoc />
    public override bool CheckHit(ref SKPoint point)
    {
        foreach (var item in GroupedItems)
        {
            if (item.CheckHit(ref point))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool CheckMouseDown(MouseParameters mouse)
    {
        foreach (var item in GroupedItems)
        {
            if (item.CheckMouseDown(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool CheckMouseUp(MouseParameters mouse)
    {
        foreach (var item in GroupedItems)
        {
            if (item.CheckMouseUp(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool CheckMouseMove(MovingMouseParameters mouse)
    {
        foreach (var item in GroupedItems)
        {
            if (item.CheckMouseMove(mouse))
            {
                return true;
            }
        }

        return false;
    }
}
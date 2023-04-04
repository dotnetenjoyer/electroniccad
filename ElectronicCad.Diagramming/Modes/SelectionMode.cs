using System.Linq;
using System.Windows.Input;
using ElectronicCad.Diagramming.Items;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;

    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (Diagram.FocusItem != null && Diagram.FocusItem is GeometryObjectDiagramItem geometryDiagramItem)
        {
            Diagram.Cursor = Cursors.Hand;
        }
        else
        {
            Diagram.Cursor = Cursors.Arrow;
        }
    }

    /// <inheritdoc/>
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var selectionFrame = GetSelectionFrame();
        
        if(Diagram.FocusItem == selectionFrame || Diagram.FocusItem is GizmoDiagramItem)
        {
            return;
        }
        
        if (Diagram.FocusItem is GeometryObjectDiagramItem geometryDiagramItem)
        {
            selectionFrame.IsVisible = true;
            selectionFrame.SelectedItem = geometryDiagramItem.GeometryObject;
        }
        else
        {
            selectionFrame.IsVisible = false;
        }

        Diagram.Redraw();
    }

    private SelectionFrameDiagramItem GetSelectionFrame()
    {
        SelectionFrameDiagramItem? selectionFrame = Diagram.DiagramItems
            .FirstOrDefault(item => item is SelectionFrameDiagramItem) as SelectionFrameDiagramItem;

        if (selectionFrame == null)
        {
            selectionFrame = new SelectionFrameDiagramItem()
            {
                IsVisible = false
            };

            Diagram.AddDiagramItem(selectionFrame);
        }

        return selectionFrame;
    }

    /// <inheritdoc/>
    public override void Finalize()
    {
        base.Finalize();
        
        var selectionFrame = GetSelectionFrame();
        selectionFrame.IsVisible = false;

        Diagram.Redraw();
    }
}
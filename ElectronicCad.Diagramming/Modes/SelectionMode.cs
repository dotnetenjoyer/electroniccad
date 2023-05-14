using System.Linq;
using System.Windows.Input;
using ElectronicCad.Diagramming.Items;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    private SelectionFrameDiagramItem selectionFrame;

    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;

    /// <inheritdoc />
    public override void Initialize(Diagram diagram)
    {
        base.Initialize(diagram);

        selectionFrame = GetOrCreateSelectionFrame();
    }

    private SelectionFrameDiagramItem GetOrCreateSelectionFrame()
    {
        var selectionFrame = Diagram.DiagramItems
            .OfType<SelectionFrameDiagramItem>()
            .FirstOrDefault();

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
        if(Diagram.FocusItem == selectionFrame)
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

    /// <inheritdoc/>
    public override void Finish()
    {
        base.Finish();
        
        selectionFrame.IsVisible = false;
        Diagram.Redraw();
    }
}
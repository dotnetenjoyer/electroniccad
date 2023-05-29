using System.Linq;
using System.Windows.Input;
using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;
using ElectronicCad.Diagramming.Drawing.Items;

namespace ElectronicCad.Diagramming.Drawing.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    private bool isSelectionStarted;

    private SelectionFrameDiagramItem selectionFrame;
    private SelectionAreaDiagramItem selectionArea;

    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;

    /// <inheritdoc />
    public override void Initialize(Diagram diagram)
    {
        base.Initialize(diagram);

        selectionArea = GetSelectionArea();
        selectionFrame = GetSelectionFrame();
    }

    private SelectionAreaDiagramItem GetSelectionArea()
    {
        var selectionArea = Diagram.DiagramItems
            .OfType<SelectionAreaDiagramItem>()
            .FirstOrDefault() ?? CreateSelectionArea();

        return selectionArea;

        SelectionAreaDiagramItem CreateSelectionArea()
        {
            var selectionArea = new SelectionAreaDiagramItem()
            {
                IsVisible = false
            };

            Diagram.AddDiagramItem(selectionArea, Diagram.SystemLayer);
            return selectionArea;
        }
    }

    private SelectionFrameDiagramItem GetSelectionFrame()
    {
        var selectionFrame = Diagram.DiagramItems
            .OfType<SelectionFrameDiagramItem>()
            .FirstOrDefault() ?? CreateSelectionFrame();

        return selectionFrame;

        SelectionFrameDiagramItem CreateSelectionFrame()
        {
            selectionFrame = new SelectionFrameDiagramItem()
            {
                IsVisible = false
            };

            Diagram.AddDiagramItem(selectionFrame, Diagram.SystemLayer);

            return selectionFrame;
        }
    }

    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (isSelectionStarted)
        {
            var position = Diagram.CalculateDiagramPosition(args);
            selectionArea.SetEndPoint(position);
            Diagram.Redraw();
        }
    }

    /// <inheritdoc/>
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        if (Diagram.FocusItem == selectionFrame)
        {
            return;
        }

        if (Diagram.FocusItem is GeometryObjectDiagramItem geometryDiagramItem)
        {
            selectionFrame.IsVisible = true;
            selectionFrame.SelectedItems = new[] { geometryDiagramItem.GeometryObject };
        }
        else
        {
            var position = Diagram.CalculateDiagramPosition(args);
            StartSelection(position);
        }

        Diagram.Redraw();
    }
    
    /// <inheritdoc/>
    protected override void ProcessPrimaryButtonUp(MouseButtonEventArgs args)
    {
        if (isSelectionStarted)
        {
            CompleteSelection();
            Diagram.Redraw();
        }
    /// <inheritdoc/>
    }

    private void StartSelection(SKPoint position)
    {
        isSelectionStarted = true;
        selectionFrame.IsVisible = false;
        selectionArea.IsVisible = true;
        selectionArea.SetStartPoint(position);
    }

    private void CompleteSelection()
    {
        isSelectionStarted = false;
        selectionArea.IsVisible = false;

        var standardizedSelectionArea = selectionArea.BoundingBox.Standardized;
        var selectedItems = Diagram.DiagramItems
            .OfType<GeometryObjectDiagramItem>()
            .Where(item => item.IsVisible)
            .Where(item => item.BoundingBox.IntersectsWith(standardizedSelectionArea))
            .Select(item => item.GeometryObject);
        
        selectionFrame.SelectedItems = selectedItems;
        selectionFrame.IsVisible = true;
    }

    /// <inheritdoc/>
    public override void Finish()
    {
        base.Finish();
    }
}
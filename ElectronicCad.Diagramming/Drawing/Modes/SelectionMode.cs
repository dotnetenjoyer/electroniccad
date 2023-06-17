using System.Linq;
using System.Windows.Input;
using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;
using ElectronicCad.Diagramming.Drawing.Items;
using System;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Drawing.DiagramItems.Extensions;

namespace ElectronicCad.Diagramming.Drawing.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    private bool isSelectionStarted;
    
    private SelectionAreaDiagramItem SelectionArea => Diagram.SelectionArea;

    private SelectionFrameDiagramItem SelectionFrame => Diagram.SelectionFrame;
    
    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;


    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (isSelectionStarted)
        {
            var position = Diagram.CalculateDiagramPosition(args);
            SelectionArea.SetEndPoint(position);
            Diagram.Redraw();
        }
    }

    /// <inheritdoc/>
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        if (Diagram.FocusItem != null && SelectionFrame.Contains(Diagram.FocusItem))
        {
            return;
        }

        if (Diagram.FocusItem is GeometryObjectDiagramItem geometryDiagramItem)
        {
            Diagram.SelectedItems = new[] { geometryDiagramItem.GeometryObject };
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
        }
    }

    private void StartSelection(SKPoint position)
    {
        isSelectionStarted = true;
        SelectionArea.IsVisible = true;
        SelectionArea.SetStartPoint(position);
    }

    private void CompleteSelection()
    {
        isSelectionStarted = false;
        SelectionArea.IsVisible = false;

        var standardizedSelectionArea = SelectionArea.BoundingBox.Standardized;
        Diagram.SelectedItems = Diagram.DiagramItems
            .Where(item => item.IsVisible)
            .Where(item => item.BoundingBox.IntersectsWith(standardizedSelectionArea))
            .OfType<GeometryObjectDiagramItem>()
            .Select(item => item.GeometryObject)
            .ToList();
    }

    /// <inheritdoc/>
    public override void Finish()
    {
        Diagram.SelectedItems = Array.Empty<GeometryObject>();
        base.Finish();
    }
}
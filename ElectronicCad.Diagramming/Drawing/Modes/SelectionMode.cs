using System.Linq;
using System.Windows.Input;
using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;
using ElectronicCad.Diagramming.Drawing.Items;
using System;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    private bool isSelectionStarted;
    private SelectionAreaDiagramItem selectionArea;

    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;

    /// <inheritdoc />
    public override void Initialize(Diagram diagram)
    {
        base.Initialize(diagram);

        selectionArea = GetSelectionArea();
    }

    private SelectionAreaDiagramItem GetSelectionArea()
    {
        var selectionArea = Diagram.DiagramItems
            .OfType<SelectionAreaDiagramItem>()
            .First();

        return selectionArea;
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
        if (Diagram.FocusItem is SelectionFrameDiagramItem)
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
            Diagram.Redraw();
        }
    }

    private void StartSelection(SKPoint position)
    {
        isSelectionStarted = true;
        selectionArea.IsVisible = true;
        selectionArea.SetStartPoint(position);
    }

    private void CompleteSelection()
    {
        isSelectionStarted = false;
        selectionArea.IsVisible = false;

        var standardizedSelectionArea = selectionArea.BoundingBox.Standardized;
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
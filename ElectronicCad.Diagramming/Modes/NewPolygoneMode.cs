using System.Windows.Input;
using SkiaSharp;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Diagram mode that creates new polygonss.  
/// </summary>
public class NewPolygonMode : BaseDiagramMode
{
    private Polygon? temporaryPolygon;
    private SKPoint startDrawingPosition;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
    }

    /// <inheritdoc />
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        
    }

    /// <inheritdoc />
    protected override void Cancel()
    {
        if(temporaryPolygon != null)
        {
            Diagram.DomainDiagram.RemoveGeometry(temporaryPolygon);
            temporaryPolygon = null;
        }
    }
}
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
        var position = Diagram.GetPosition(args);

        if(temporaryPolygon == null)
        {
            startDrawingPosition = position;

            var domainPoint = position.ToDomainPoint();
            temporaryPolygon = new Polygon(domainPoint, domainPoint, domainPoint, domainPoint);
            temporaryPolygon.IsTemporary = true;
            
            Diagram.DomainDiagram.AddGeometry(temporaryPolygon);
        }
        else
        {
            temporaryPolygon.IsTemporary = true;
            temporaryPolygon = null;
            startDrawingPosition = SKPoint.Empty;
        }
    }

    /// <inheritdoc />
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if(temporaryPolygon == null)
        {
            return;
        }

        var position = Diagram.GetPosition(args);
        var delta = position - startDrawingPosition;

        using var modificationScope = Diagram.DomainDiagram.StartModification();
        temporaryPolygon.UpdateControlPoint(ContentGeometry.LeftTopPointIndex, startDrawingPosition.X - delta.X, startDrawingPosition.Y - delta.Y);
        temporaryPolygon.UpdateControlPoint(ContentGeometry.RigthTopPointIndex, startDrawingPosition.X + delta.X, startDrawingPosition.Y - delta.Y);
        temporaryPolygon.UpdateControlPoint(ContentGeometry.RigthBottomPointIndex, startDrawingPosition.X + delta.X, startDrawingPosition.Y + delta.Y);
        temporaryPolygon.UpdateControlPoint(ContentGeometry.LeftBottomPointIndex, startDrawingPosition.X - delta.X, startDrawingPosition.Y + delta.Y);
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
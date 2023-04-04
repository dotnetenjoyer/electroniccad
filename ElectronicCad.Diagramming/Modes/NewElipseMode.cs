using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;
using System.Windows.Input;

namespace ElectronicCad.Diagramming.Modes;

internal class NewEllipseMode : BaseDiagramMode
{
    private Ellipse? temporaryEllipse;
    private SKPoint startDrawingPosition;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.GetPosition(args);

        if (temporaryEllipse == null)
        {
            startDrawingPosition = position;

            var domainPoint = position.ToDomainPoint();
            temporaryEllipse = new Ellipse(domainPoint, domainPoint, domainPoint, domainPoint)
            {
                IsTemporary = true
            };
            Diagram.DomainDiagram.AddGeometry(temporaryEllipse);
        }
        else
        {
            temporaryEllipse.IsTemporary = false;
            temporaryEllipse = null;
         
            startDrawingPosition = SKPoint.Empty;
        }
    }
    
    /// <inheritdoc />
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        var position = Diagram.GetPosition(args);

        if(temporaryEllipse == null)
        {
            return;
        }

        var delta = position - startDrawingPosition;
        
        using var modificationScope = Diagram.DomainDiagram.StartModification();
        temporaryEllipse.UpdateControlPoint(ContentGeometry.LeftTopPointIndex, startDrawingPosition.X - delta.X, startDrawingPosition.Y - delta.Y);
        temporaryEllipse.UpdateControlPoint(ContentGeometry.RigthTopPointIndex, startDrawingPosition.X + delta.X, startDrawingPosition.Y - delta.Y);
        temporaryEllipse.UpdateControlPoint(ContentGeometry.RigthBottomPointIndex, startDrawingPosition.X + delta.X, startDrawingPosition.Y + delta.Y);
        temporaryEllipse.UpdateControlPoint(ContentGeometry.LeftBottomPointIndex, startDrawingPosition.X - delta.X, startDrawingPosition.Y + delta.Y);
    }

    /// <inheritdoc />
    protected override void Cancel()
    {
        if (temporaryEllipse != null)
        {
            Diagram.DomainDiagram.RemoveGeometry(temporaryEllipse);
            temporaryEllipse = null;
        }
    }
}
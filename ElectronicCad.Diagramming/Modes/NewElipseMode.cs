using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using System;
using System.Numerics;
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

        using var modificationScope = Diagram.DomainDiagram.StartModification();
        var delta = position - startDrawingPosition;

        temporaryEllipse.SetControlPoint(ContentGeometry.LeftTopPointIndex, startDrawingPosition.X - delta.X, startDrawingPosition.Y - delta.Y);
        temporaryEllipse.SetControlPoint(ContentGeometry.RigthTopPointIndex, startDrawingPosition.X + delta.X, startDrawingPosition.Y - delta.Y);
        temporaryEllipse.SetControlPoint(ContentGeometry.RigthBottomPointIndex, startDrawingPosition.X + delta.X, startDrawingPosition.Y + delta.Y);
        temporaryEllipse.SetControlPoint(ContentGeometry.LeftBottomPointIndex, startDrawingPosition.X - delta.X, startDrawingPosition.Y + delta.Y);
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
using System;
using System.Windows.Input;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Diagram mode that creates new polygonss.  
/// </summary>
internal class PolygonCreationMode : ShapeCreationMode<Polygon>
{
    public const double InitialSize = 40;

    /// <inheritdoc />
    protected override Polygon CreateActualElement()
    {
        var actualElement = new Polygon(TemporaryElement!.BoundingBox.Center, TemporaryElement.BoundingBox.Width, TemporaryElement.BoundingBox.Width);
        return actualElement;
    }

    /// <inheritdoc />
    protected override Polygon CreateTemporaryElement(Point centerPoint)
    {
        var temporaryElement = new Polygon(centerPoint, InitialSize, InitialSize, true);
        return temporaryElement;
    }

    /// <inheritdoc />
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (IsCreationStart && TemporaryElement != null)
        {
            var position = Diagram.GetPosition(args).ToDomainPoint();
            var deltaLength = (position - TemporaryElement.BoundingBox.Center).CalculateLength();

            // square inscribed in a circle.
            var polygonSize = Math.Sqrt(2 * deltaLength * deltaLength);

            using var scope = TemporaryElement.StartDiagramModifcation();
            TemporaryElement.StartModification();
            TemporaryElement.SetCenterAndSize(TemporaryElement.BoundingBox.Center, polygonSize, polygonSize);
            TemporaryElement.CompleteModification();
        }
    }

    /// <inheritdoc />
    protected override void DrawGizmos(SkiaDrawingContext drawingContext)
    {
        if (IsCreationStart && TemporaryElement != null)
        {
            var gizmosPaint = new SKPaint()
            {
                Color = SKColors.White,
                Style = SKPaintStyle.Stroke,
                PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 0),
            };

            var center = TemporaryElement.BoundingBox.Center.ToSKPoint();
            var radius = Math.Sqrt(TemporaryElement.BoundingBox.Width * TemporaryElement.BoundingBox.Width / 2);
            drawingContext.DrawEllipse(center, radius, gizmosPaint);
        }
    }
}
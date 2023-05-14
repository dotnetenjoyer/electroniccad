﻿using System.Windows.Input;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// The diagram mode to create new ellipse.
/// </summary>
internal class EllipseCreationMode : ShapeCreationMode<Ellipse>
{
    public const double InitialRadius = 10; 

    /// <inheritdoc />
    protected override Ellipse CreateActualElement()
    {
        var actualElement = new Ellipse(TemporaryElement!.BoundingBox.Center, TemporaryElement.RadiusX);
        return actualElement;
    }

    /// <inheritdoc />
    protected override Ellipse CreateTemporaryElement(Point centerPoint)
    {
        var temporaryEllement = new Ellipse(centerPoint, InitialRadius, isTemporary: true);
        return temporaryEllement;
    }

    /// <inheritdoc />
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (IsCreationStart && TemporaryElement != null)
        {
            var position = Diagram.GetPosition(args).ToDomainPoint();
            var radius = (position - TemporaryElement!.BoundingBox.Center).CalculateLength();

            using var modificationScope = Diagram.DomainDiagram.StartModification();
            TemporaryElement.StartModification();
            TemporaryElement.SetCenterAndRadius(TemporaryElement!.BoundingBox.Center, radius, radius);
            TemporaryElement.CompleteModification();
        }
    }
}
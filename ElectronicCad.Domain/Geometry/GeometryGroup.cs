﻿using System.Numerics;
using ElectronicCad.Domain.Geometry.Utils;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Group of geometry objects.
/// </summary>
public class GeometryGroup : ContentGeometry, IGeometryContainer
{
    /// <inheritdoc />
    public override string Name { get; init; } = "Group";
    
    /// <inheritdoc />
    public IEnumerable<GeometryObject> Children => children;

    private readonly List<GeometryObject> children = new();

    /// <inheritdoc />
    public override Layer? Layer 
    { 
        get => layer;
        internal set 
        {
            layer = value;

            foreach (var child in Children)
            {
                child.Layer = value;
            }
        }
    }

    private Layer? layer;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObjects">Grouped geometry objects.</param>
    public GeometryGroup(IEnumerable<GeometryObject> geometryObjects)
    {
        AddGeometry(geometryObjects);
    }

    /// <inheritdoc />
    protected override void TransformInternal(Matrix3x2 transformation)
    {
        foreach (var child in Children)
        {
            child.StartModification();
            child.Transform(transformation);
            child.CompleteModification();
        }
    }

    /// <inheritdoc />
    public void AddGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Layer = Layer;
            geometryObject.Group = this;
            geometryObject.VersionChanged += HandleGeometryVersionChange;
            children.Add(geometryObject);
        }

        RecalculateControlPoints();
        Diagram?.RaiseGeometryAdded(geometryObjects);
        IncrementVersion();
    }

    /// <inheritdoc />
    public void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Layer = null;
            geometryObject.Group = null;
            geometryObject.VersionChanged -= HandleGeometryVersionChange;
            children.Remove(geometryObject);
        }

        RecalculateControlPoints();
        Diagram?.RaiseGeometryRemoved(geometryObjects);
        IncrementVersion();
    }

    private void HandleGeometryVersionChange(object? sender, EventArgs eventArgs)
    {
        RecalculateControlPoints();
        IncrementVersion();
    }
   
    private void RecalculateControlPoints()
    {
        var childrenPoints = Children.SelectMany(x => x.ControlPoints);
        var boundingBox = PointsUtils.CalculateBoundingBox(childrenPoints);

        var topLeft = boundingBox.Start;
        var topRight = new Point(boundingBox.End.X, boundingBox.Start.Y);
        var bottomLeft = new Point(boundingBox.Start.X, boundingBox.End.Y);
        var bottomRight = boundingBox.End;
        var center = boundingBox.Center;

        SetControlPoints(center, topLeft, topRight, bottomLeft, bottomRight);
    }
}
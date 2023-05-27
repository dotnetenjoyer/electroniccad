using System.Numerics;
using ElectronicCad.Domain.Geometry.Utils;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Group of geometry objects.
/// </summary>
public class GeometryGroup : ContentGeometry, IGeometryContainer
{
    /// <inheritdoc />
    public override string Name { get; init; } = "Group";

    /// <summary>
    /// Children geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> Children => children;

    private readonly List<GeometryObject> children = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="children">Grouped geometry objects.</param>
    public GeometryGroup(IEnumerable<GeometryObject> children)
    {
        AddGeometry(children);
        RecalculateControlPoints();
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
    public void AddGeometry(GeometryObject geometryObject)
    {
        AddGeometry(new[] { geometryObject });
    }

    /// <inheritdoc />
    public void AddGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Parent = this;
            geometryObject.VersionChanged += HandleGeometryVersionChange;
            children.Add(geometryObject);
        }

        RecalculateControlPoints();
        IncrementVersion();
    }

    /// <inheritdoc />
    public void RemoveGeometry(GeometryObject geometryObject)
    {
        RemoveGeometry(new[] { geometryObject });
    }

    /// <inheritdoc />
    public void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Parent = null;
            geometryObject.VersionChanged -= HandleGeometryVersionChange;
            children.Remove(geometryObject);
        }

        RecalculateControlPoints();
        IncrementVersion();
    }

    private void HandleGeometryVersionChange(object? sender, EventArgs eventArgs)
    {
        RecalculateControlPoints();
        IncrementVersion();
    }
}
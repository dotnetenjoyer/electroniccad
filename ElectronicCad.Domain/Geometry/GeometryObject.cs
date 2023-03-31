using System;
using System.Drawing;

namespace ElectronicCad.Domain.Geometry;

public abstract class GeometryObject
{
    /// <summary>
    /// Geometry object id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public virtual string Name { get; init; }

    /// <summary>
    /// Control points.
    /// </summary>
    public IReadOnlyList<Point> ControlPoints => controlPoints;

    /// <summary>
    /// Set of geometry control points.
    /// </summary>
    protected Point[] controlPoints;

    /// <summary>
    /// Related layer.
    /// </summary>
    public Layer? Layer { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected GeometryObject()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Updates control point values.
    /// </summary>
    /// <param name="index">Index of control point.</param>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    public void UpdateControlPoint(int index, double x, double y)
    {
        ValidateModification();

        var point = controlPoints[index];
        if(point != null)
        {
            point.SetValues(x, y);
        }
        else
        {
            controlPoints[index] = new Point(x, y);
        }

        Layer!.Diagram.ModificationScope!.AddModifiedItem(this);
    }

    private void ValidateModification()
    {
        if(Layer == null || Layer.Diagram == null || Layer.Diagram.ModificationScope == null)
        {
            throw new Exception("Modification outisde scope are prohibited.");
        }
    }

    /// <summary>
    /// Check hit to geometry.
    /// </summary>
    /// <param name="point">Target point to hit.</param>
    /// <returns><c>true</c> if point hit geomtry.</returns>
    public virtual bool CheckHit(PointF point) => CheckHitToBoundingBox(point);

    /// <summary>
    /// Check hit to geometry bounding box.
    /// </summary>
    /// <param name="point">Target point ot hit</param>
    /// <returns><c>true</c> if point hit bounding box.</returns>
    public bool CheckHitToBoundingBox(PointF point)
    {
        var boundingBox = CalculateBoundingBox();
        return boundingBox.Contains(point);
    }

    /// <summary>
    /// Calculates objects bounding box.
    /// </summary>
    public RectangleF CalculateBoundingBox()
    {
        return RectangleF.Empty;
        //return CalculateBoundingBox(ControlPoints);
    }

    /// <summary>
    /// Calculates bounding box base on set of points.
    /// </summary>
    /// <param name="points">Set of points.</param>
    /// <returns>Bounding box.</returns>
    private RectangleF CalculateBoundingBox(PointF[] points)
    {
        if(points.Length == 0)
        {
            return RectangleF.Empty;
        }

        float 
            maxX = points[0].X, 
            minX = points[0].X, 
            maxY = points[0].Y,
            minY = points[0].Y;

        foreach (var point in points)
        {
            maxX = Math.Max(maxX, point.X);
            minX = Math.Min(minX, point.X);
            maxY = Math.Max(maxY, point.Y);
            minY = Math.Min(minY, point.Y);
        }

        var width = maxX - minX;
        var height = maxY - minY;
        return new RectangleF(minX, minY, width, height);
    }
}
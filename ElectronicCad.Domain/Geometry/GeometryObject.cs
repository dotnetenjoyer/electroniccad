using System.Numerics;
using ElectronicCad.Domain.Common;
using ElectronicCad.Domain.Exceptions;
using ElectronicCad.Domain.Geometry.Utils;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// The class represents a simple geometry objects that can be drawn from lines.
/// </summary>
public abstract class GeometryObject : VersionableBase, IHaveName
{
    /// <summary>
    /// Geometry object id.
    /// </summary>
    public Guid Id { get; internal set; } = Guid.NewGuid();

    /// <summary>
    /// Name.
    /// </summary>
    public string Name 
    {
        get => name; 
        internal set => SetProperty(ref name, value);
    }

    protected string name = "Geometry object";

    /// <summary>
    /// Control points.
    /// </summary>
    public IReadOnlyList<Point> ControlPoints => controlPoints;

    /// <summary>
    /// Set of geometry control points.
    /// </summary>
    protected Point[] controlPoints;

    /// <summary>
    /// Geometry object bounding box.
    /// </summary>
    public Rectangle BoundingBox 
    { 
        get => boundingBox; 
        private set => SetProperty(ref boundingBox, value); 
    }

    private Rectangle boundingBox;

    /// <summary>
    /// Related layer.
    /// </summary>
    public virtual Layer? Layer { get; internal set; }

    /// <summary>
    /// Related diagram.
    /// </summary>
    public Diagram? Diagram => Layer?.Diagram;

    /// <summary>
    /// Geometry group.
    /// </summary>
    public GeometryGroup? Group
    {
        get => group;
        internal set => SetProperty(ref group, value);
    }

    private GeometryGroup? group;
    
    /// <summary>
    /// Stroke color.
    /// </summary>
    public Color StrokeColor
    {
        get => strokeColor;
        set
        {
            ValidateModification();
            strokeColor = value;
        }
    }

    private Color strokeColor = Theme.Foreground;

    /// <summary>
    /// Determines the geometry object stoke thickness.
    /// </summary>
    public double StrokeWidth 
    { 
        get => strokeWidth;
        set
        {
            ValidateModification();
            SetProperty(ref strokeWidth, value);
        }
    }

    private double strokeWidth = 2;

    /// <summary>
    /// Indicates if geometry object is temporary.
    /// </summary>
    public bool IsTemporary
    {
        get => isTemporary;
        set
        {
            ValidateModification();
            SetProperty(ref isTemporary, value);
        }
    }

    private bool isTemporary;

    /// <summary>
    /// Indicates if geometry object is visible.
    /// </summary>
    public bool IsVisible 
    { 
        get => isVisible;
        set
        {
            ValidateModification();
            SetProperty(ref isVisible, value);
        }
    }

    private bool isVisible = true;

    /// <summary>
    /// Indicates if geometry object is locked.
    /// </summary>
    public bool IsLock
    {
        get => isLock;
        set
        {
            ValidateModification();
            SetProperty(ref isLock, value);
        }
    }

    private bool isLock;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GeometryObject(bool isTemporary = false)
    {
        this.isTemporary = isTemporary;
    }

    /// <summary>
    /// Clone constructor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public GeometryObject(GeometryObject cloneFrom)
    {
        Name = cloneFrom.Name;
        strokeWidth = cloneFrom.StrokeWidth;
        strokeColor = new Color(cloneFrom.StrokeColor);
        isTemporary = cloneFrom.IsTemporary;
        
        controlPoints = cloneFrom.ControlPoints
            .Select(p => new Point(p.X, p.Y))
            .ToArray();

        RecalculateBoundingBox();
    }

    #region Versioning

    /// <inhertidoc />
    protected override void ValidateModification()
    {
        if (!IsModificationStarted)
        {
            throw new DomainException("Modification is not started.");
        }

        if (Diagram == null || Diagram.ModificationScope == null)
        {
            throw new DomainException("Modification outisde scope are prohibited.");
        }

        Diagram.ModificationScope.AddModifiedItem(this);
    }

    /// <summary>
    /// Starts diagram modification.
    /// </summary>
    /// <returns>Diagram modification scope.</returns>
    public DiagramModificationScope StartDiagramModifcation()
    {
        if (Diagram == null)
        {
            throw new DomainException("Geometry object is not related with diagram.");
        }

        return Diagram.StartModificationScope();
    }

    #endregion

    /// <summary>
    /// Set control points without modification validation.
    /// </summary>
    /// <param name="points">Points to set.</param>
    internal void SetControlPoints(params Point[] points)
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            controlPoints[i] = points[i];
        }

        RecalculateBoundingBox();
    }

    /// <summary>
    /// Transforms geometry object with transfromation matrix.
    /// </summary>
    /// <param name="transformation">Transformation matrix.</param>
    public void Transform(Matrix3x2 transformation)
    {
        ValidateModification();
        TransformInternal(transformation);
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Contains logic to transform geometry object, can be overrided.
    /// </summary>
    /// <param name="transformation">Transformations.</param>
    protected virtual void TransformInternal(Matrix3x2 transformation)
    {
        for (int i = 0; i < ControlPoints.Count; i++)
        {
            var point = ControlPoints[i].ToVector2();
            var newPoint = Vector2.Transform(point, transformation);
            SetControlPoint(i, newPoint.X, newPoint.Y, false);
        }
    }

    /// <summary>
    /// Set control point values.
    /// </summary>
    /// <param name="index">Index of control point.</param>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    public void SetControlPoint(int index, double x, double y)
    {
        SetControlPoint(index, x, y, true);
        RecalculateBoundingBox();
    }

    private void SetControlPoint(int index, double x, double y, bool validateModification)
    {
        if (validateModification)
        {
            ValidateModification();
        }

        controlPoints[index].SetValues(x, y);
    }

    /// <summary>
    /// Set center point and size of the geometry object.
    /// </summary>
    /// <param name="centerX">Center coordinate X.</param>
    /// <param name="centerY">Center coordinate Y.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public void SetCenterAndSize(double centerX, double centerY, double width, double height)
    {
        SetCenterAndSize(new Point(centerX, centerY), width, height);
    }

    /// <summary>
    /// Set center point and size of the geometry object.
    /// </summary>
    /// <param name="center">Center position.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public void SetCenterAndSize(Point center, double width, double height)
    {
        var translationToOrigin = Matrix3x2.CreateTranslation(-BoundingBox.Center.ToVector2());

        var scaleVector = new Vector2((float)(width / BoundingBox.Width), (float)(height / BoundingBox.Height));
        var scaleTransformation = Matrix3x2.CreateScale(scaleVector);

        var translationToNewPosition = Matrix3x2.CreateTranslation(center.ToVector2());
        
        var transformation = translationToOrigin * scaleTransformation * translationToNewPosition;
        Transform(transformation);
    }

    /// <summary>
    /// Check hit to geometry.
    /// </summary>
    /// <param name="point">Target point to hit.</param>
    /// <returns><c>true</c> if point hit geomtry.</returns>
    public virtual bool CheckHit(Point point)
    {
        return CheckBoundingBoxHit(point);
    }

    /// <summary>
    /// Check hit to geometry bounding box.
    /// </summary>
    /// <param name="point">Target point ot hit</param>
    /// <returns><c>true</c> if point hit bounding box.</returns>
    public bool CheckBoundingBoxHit(Point point)
    {
        return BoundingBox.Contains(point);
    }

    /// <summary>
    /// Recalculates bounding box.
    /// </summary>
    protected void RecalculateBoundingBox()
    {
        BoundingBox = CalculateBoundingBoxInternal();
    }

    /// <summary>
    /// Calculates bounding box.
    /// </summary>
    /// <returns>Bounding box.</returns>
    protected virtual Rectangle CalculateBoundingBoxInternal()
    {
        return PointsUtils.CalculateBoundingBox(controlPoints);
    }
}
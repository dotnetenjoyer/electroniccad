using ElectronicCad.Domain.Common;
using ElectronicCad.Domain.Exceptions;
using ElectronicCad.Domain.Geometry.Utils;
using System.Numerics;

namespace ElectronicCad.Domain.Geometry;

public abstract class GeometryObject : DomainObservableObject, IVersionable
{
    /// <summary>
    /// Geometry object id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public virtual string Name { get; init; } = "Geometry object";

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
    public Rectangle BoundingBox { get; private set; }

    /// <summary>
    /// Related layer.
    /// </summary>
    public Layer? Layer { get; internal set; }

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

    private Color strokeColor = Color.White;

    /// <summary>
    /// Determines the geometry object stoke thickness.
    /// </summary>
    public double StrokeWidth 
    { 
        get => strokeWidth;
        set
        {
            ValidateModification();
            strokeWidth = value;
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
            IsTemporary = value;
        }
    }

    private bool isTemporary;

    #region Versioning

    /// <inheritdoc />
    public int Version { get; private set; }

    /// <inheritdoc />
    public event EventHandler? VersionChanged;

    /// <summary>
    /// Increases the version of the geometry object and invokes the <see cref="VersionChanged">.
    /// </summary>
    public void IncreaseVersion()
    {
        Version++;
        VersionChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion
    
    /// <summary>
    /// Indicates if modification was started.
    /// </summary>
    public bool IsModificationStarted => isModificationStarted;

    private bool isModificationStarted;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GeometryObject(bool isTemporary = false)
    {
        Id = Guid.NewGuid();
        this.isTemporary = isTemporary;
    }

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

        for (int i = 0; i < ControlPoints.Count; i++)
        {
            var point = ControlPoints[i].ToVector2();
            var newPoint = Vector2.Transform(point, transformation);
            SetControlPoint(i, newPoint.X, newPoint.Y, false);
        }

        RecalculateBoundingBox();
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
    /// Validates availability of modification.
    /// </summary>
    protected void ValidateModification()
    {
        if (Layer == null || Layer.Diagram == null || Layer.Diagram.ModificationScope == null)
        {
            throw new DomainException("Modification outisde scope are prohibited.");
        }

        if (!isModificationStarted)
        {
            throw new DomainException("Modification is not started.");
        }

        Layer!.Diagram.ModificationScope!.AddModifiedItem(this);
    }

    /// <summary>
    /// Starts diagram modification.
    /// </summary>
    /// <returns>Diagram modification scope.</returns>
    public DiagramModificationScope StartDiagramModifcation()
    {
        if (Layer == null || Layer.Diagram == null)
        {
            throw new DomainException("The geometry object isn't related with a diagram.");
        }

        return Layer.Diagram.StartModification();
    }

    /// <summary>
    /// Starts object modification.
    /// </summary>
    public void StartModification()
    {
        if (isModificationStarted)
        {
            throw new DomainException("Attempting to start a modification when it is already started.");
        }

        isModificationStarted = true;
    }

    /// <summary>
    /// Completes object modification.
    /// </summary>
    public void CompleteModification()
    {   
        if (!isModificationStarted)
        {
            throw new DomainException("Attempting to complete a modification when it is not started.");
        }

        isModificationStarted = false;
        IncreaseVersion();
    }

    /// <summary>
    /// Check hit to geometry.
    /// </summary>
    /// <param name="point">Target point to hit.</param>
    /// <returns><c>true</c> if point hit geomtry.</returns>
    public virtual bool CheckHit(Point point) => CheckBoundingBoxHit(point);

    /// <summary>
    /// Check hit to geometry bounding box.
    /// </summary>
    /// <param name="point">Target point ot hit</param>
    /// <returns><c>true</c> if point hit bounding box.</returns>
    public bool CheckBoundingBoxHit(Point point)
    {
        return BoundingBox.Contains(point);
    }

    private void RecalculateBoundingBox()
    {
        BoundingBox = PointsUtils.CalculateBoundingBox(controlPoints);
    }
}
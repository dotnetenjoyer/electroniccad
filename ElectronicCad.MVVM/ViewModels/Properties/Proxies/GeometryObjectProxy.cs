using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Geometry object property proxy.
/// </summary>
public abstract class GeometryObjectProxy<TGeometryObject> : VersionablePropertiesProxy<TGeometryObject>, ITransformationProxy 
    where TGeometryObject : GeometryObject
{
    /// <inheritdoc />
    public double CenterX { get; set; }

    /// <inheritdoc />
    public double CenterY { get; set; }

    /// <inheritdoc />
    public double Width { get; set; }

    /// <inheritdoc />
    public double Height { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryObjectProxy(TGeometryObject geometryObject) : base(geometryObject)
    {
    }

    /// <inheritdoc />
    public override void UpdateFromSource()
    {
        CenterX = Source.BoundingBox.Center.X; 
        CenterY = Source.BoundingBox.Center.Y;
        Width = Source.BoundingBox.Width;
        Height = Source.BoundingBox.Height;
    }
    
    /// <inheritdoc />
    public sealed override void UpdateSource()
    {
        using var scope = Source.StartDiagramModifcation();
        Source.StartModification();
        UpdateSourceInternal();
        Source.CompleteModification();
    }

    /// <inheritdoc />
    protected override void UpdateSourceInternal()
    {
        Source.SetCenterAndSize(CenterX, CenterY, Width, Height);
    }
}

using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Geometry object property proxy.
/// </summary>
public abstract class GeometryObjectProxy<TGeometryObject> : BaseProxy<TGeometryObject>, IPropertyModel, ITransformationProxy 
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
    public override void UpdateFromEntity()
    {
        CenterX = Source.BoundingBox.Center.X; 
        CenterY = Source.BoundingBox.Center.Y;
        Width = Source.BoundingBox.Width;
        Height = Source.BoundingBox.Height;
    }
    
    /// <inheritdoc />
    public sealed override void UpdateEntity()
    {
        using var scope = Source.StartDiagramModifcation();
        Source.StartModification();
        UpdateEntityInternal();
        Source.CompleteModification();
    }

    /// <summary>
    /// Updates the state of the source entity.
    /// </summary>
    protected virtual void UpdateEntityInternal()
    {
        Source.SetCenterAndSize(CenterX, CenterY, Width, Height);
    }
}

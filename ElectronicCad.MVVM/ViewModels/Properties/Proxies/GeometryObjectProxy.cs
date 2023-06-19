using ElectronicCad.Domain.Exceptions;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Extensions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Geometry object properties proxy.
/// </summary>
public abstract class GeometryObjectPropertiesProxy<TGeometryObject> : VersionablePropertiesProxy<TGeometryObject>, ITransformationProxy 
    where TGeometryObject : GeometryObject
{
    /// <summary>
    /// Geometry object name.
    /// </summary>
    public string Name { get; set; }

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
    public GeometryObjectPropertiesProxy(TGeometryObject geometryObject) : base(geometryObject)
    {
    }

    /// <inheritdoc />
    public override void UpdateFromSource()
    {
        Name = Source.Name;
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
        Source.Rename(Name);
        Source.SetCenterAndSize(CenterX, CenterY, Width, Height);
    }
}

using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Geometry object property proxy.
/// </summary>
public abstract class GeometryObjectPropertyProxy<TGeometryObject> : PropertyProxy<TGeometryObject>, IPropertyModel 
    where TGeometryObject : GeometryObject
{
    /// <summary>
    /// Width.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// Height
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Center X coordinate.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Center Y coordinate
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryObjectPropertyProxy(TGeometryObject geometryObject) : base(geometryObject)
    {
    }

    /// <inheritdoc />
    public override void UpdateFromEntity()
    {
        var boundingBox = Source.CalculateBoundingBox();

        X = boundingBox.X; 
        Y = boundingBox.Y;
        Width = boundingBox.Width;
        Height = boundingBox.Height;
    }
    
    /// <inheritdoc />
    public override void UpdateToEntity()
    {
        throw new NotImplementedException();
    }
}

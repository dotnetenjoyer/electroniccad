using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Geometry object property proxy.
/// </summary>
public abstract class GeometryObjectProxy<TGeometryObject> : BaseProxy<TGeometryObject>, IPropertyModel, ITransformationProxy, IShapeProxy 
    where TGeometryObject : GeometryObject
{
    /// <inheritdoc />
    public float X { get; set; }

    /// <inheritdoc />
    public float Y { get; set; }

    /// <inheritdoc />
    public float Width { get; set; }

    /// <inheritdoc />
    public float Height { get; set; }

    /// <inheritdoc />
    public Color FillColor { get; set; }
    
    /// <inheritdoc />
    public Color StrokeColor { get; set; }
    
    /// <inheritdoc />
    public float StrokeWidth { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryObjectProxy(TGeometryObject geometryObject) : base(geometryObject)
    {
        geometryObject.VersionChanged += HandleVersionChanged;
    }

    private void HandleVersionChanged(object? sender, EventArgs eventArgs)
    {
        UpdateFromEntity();
    }


    /// <inheritdoc />
    public override void UpdateFromEntity()
    {
        var boundingBox = Source.CalculateBoundingBox();

        X = boundingBox.X + boundingBox.Width / 2; 
        Y = boundingBox.Y + boundingBox.Height / 2;
        Width = boundingBox.Width;
        Height = boundingBox.Height;
        FillColor = Source.Fill;
        StrokeColor = Source.Stroke;
        StrokeWidth = Source.StrokeWidth;

        OnUpdateFromEntity();
    }
    
    /// <inheritdoc />
    public override void UpdateEntity()
    {
        // TODO: Consider unsubscribing from version changes when updating an object.
        using var scope = Source.Layer.Diagram.StartModification();
        Source.Stroke = StrokeColor;
        Source.Fill = FillColor;
        Source.StrokeWidth = StrokeWidth;
        Source.UpdateBoundingBox(X, Y, Width, Height);
    }
}

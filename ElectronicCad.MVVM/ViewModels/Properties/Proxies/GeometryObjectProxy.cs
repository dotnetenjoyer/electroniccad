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
    public float X { get; set; }

    /// <inheritdoc />
    public float Y { get; set; }

    /// <inheritdoc />
    public float Width { get; set; }

    /// <inheritdoc />
    public float Height { get; set; }

    /// <summary>
    /// Geometry object stroke color
    /// </summary>
    public string StrokeColor { get; set; }

    /// <summary>
    /// Geometry object fill color
    /// </summary>
    public string FillColor { get; set; }

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
        StrokeColor = Source.Stroke;
        FillColor = Source.Fill;

        OnUpdateFromEntity();
    }
    
    /// <inheritdoc />
    public override void UpdateEntity()
    {
        using var scope = Source.Layer.Diagram.StartModification();
        Source.UpdateBoundingBox(X, Y, Width, Height);
        Source.Stroke = StrokeColor;
        Source.Fill = FillColor;
    }
}

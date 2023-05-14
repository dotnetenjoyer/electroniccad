using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

public abstract class ContentGeometryObjectProxy<TContentGeometry> : GeometryObjectProxy<TContentGeometry>, IShapeProxy 
    where TContentGeometry : ContentGeometry
{
    /// <inheritdoc />
    public Color FillColor { get; set; }

    /// <inheritdoc />
    public Color StrokeColor { get; set; }

    /// <inheritdoc />
    public double StrokeWidth { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="contentGeometry">Content geometry object.</param>
    public ContentGeometryObjectProxy(TContentGeometry contentGeometry) : base(contentGeometry)
    {
    }

    /// <inheritdoc />
    public override void UpdateFromEntity()
    {
        base.UpdateFromEntity();

        FillColor = Source.FillColor;
        StrokeColor = Source.StrokeColor;
        StrokeWidth = Source.StrokeWidth;
    }
    
    /// <inheritdoc />
    protected override void UpdateEntityInternal()
    {
        base.UpdateEntityInternal();

        Source.FillColor = FillColor;
        Source.StrokeColor = StrokeColor;
        Source.StrokeWidth = StrokeWidth;
    }
}
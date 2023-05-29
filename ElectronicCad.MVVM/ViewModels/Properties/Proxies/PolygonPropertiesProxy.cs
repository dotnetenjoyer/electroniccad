using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Polygone properties proxy.
/// </summary>
public class PolygonPropertiesProxy : ContentGeometryObjectProxy<Polygon>
{
    /// <summary>
    /// Polygon corner radius.
    /// </summary>
    public double CornerRadius { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="polygon">Polygon.</param>
    public PolygonPropertiesProxy(Polygon polygon) : base(polygon)
    {
    }

    /// <inheritdoc />
    public override void UpdateFromSource()
    {
        base.UpdateFromSource();

        CornerRadius = Source.CornerRadius;
    }

    /// <inheritdoc />
    protected override void UpdateSourceInternal()
    {
        base.UpdateSourceInternal();

        Source.CornerRadius = CornerRadius;
    }
}
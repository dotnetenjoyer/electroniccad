using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Properties proxy to geometry group.
/// </summary>
public class GeometryGroupPropertiesProxy : BaseProxy<GeometryGroup>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="group">Geometry group.</param>
    public GeometryGroupPropertiesProxy(GeometryGroup group) : base(group)
    {
    }

    /// <inheritdoc />
    public override void UpdateEntity()
    {
    }

    /// <inheritdoc />
    public override void UpdateFromEntity()
    {
    }
}

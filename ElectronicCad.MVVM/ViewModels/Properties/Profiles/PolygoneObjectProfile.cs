using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates configuration for rectangle.
/// </summary>
internal class PolygoneObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PolygoneObjectProfile()
    {
        CreateConfiguration<PolygonPropertyProxy>()
            .HasPrimitive(source => source.X)
            .HasPrimitive(source => source.Y)
            .HasPrimitive(source => source.Width)
            .HasPrimitive(source => source.Height)
            .HasPrimitive(source => source.StrokeColor)
            .HasPrimitive(source => source.FillColor);
    }
}

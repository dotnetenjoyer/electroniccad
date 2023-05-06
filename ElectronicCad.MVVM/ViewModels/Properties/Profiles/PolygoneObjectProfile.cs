using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Colors;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
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
            .HasCustomSection<TransformationCustomSection>()
            .HasCustomSection<ColorsCustomSection>();
    }
}

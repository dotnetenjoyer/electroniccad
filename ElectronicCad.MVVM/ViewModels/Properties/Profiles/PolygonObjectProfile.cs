using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates properties configuration for rectangle.
/// </summary>
internal class PolygonObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PolygonObjectProfile()
    {
        CreateConfiguration<PolygonPropertiesProxy>()
            .HasCustomSection<TransformationCustomSection>()
            .HasCustomSection<ShapeCustomSection>()
            .HasPrimitive(source => source.CornerRadius);
    }
}

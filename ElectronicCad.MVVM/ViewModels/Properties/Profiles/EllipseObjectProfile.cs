using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates configuration for ellipse.
/// </summary>
internal class EllipseObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public EllipseObjectProfile()
    {
        CreateConfiguration<EllipsePropertyProxy>()
            .HasCustomSection<TransformationCustomSection>()
            .HasCustomSection<ShapeCustomSection>();
    }
}
using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates image properties configuration.
/// </summary>
internal class ImageProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ImageProfile()
    {
        CreateConfiguration<ImagePropertiesProxy>()
            .HasCustomSection<TransformationCustomSection>();
    }
}
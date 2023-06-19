using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates properties configuration for text.
/// </summary>
internal class TextObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TextObjectProfile()
    {
        CreateConfiguration<TextPropertiesProxy>()
            .HasCustomSection<TransformationCustomSection>()
            .HasCustomSection<TypographyCustomSection>()
            .HasPrimitive(source => source.Name, options => options.HasName("Наименование"));
    }
}
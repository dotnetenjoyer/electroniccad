using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates configuration for line.
/// </summary>
internal class LineObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LineObjectProfile()
    {
        CreateConfiguration<LinePropertyProxy>()
            .HasCustomSection<TransformationCustomSection>()
            .HasPrimitive(source => source.Name, options => options.HasName("Наименование"));
    }
}
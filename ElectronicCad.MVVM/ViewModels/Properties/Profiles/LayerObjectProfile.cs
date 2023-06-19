using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Configurates layer properties.
/// </summary>
public class LayerObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LayerObjectProfile()
    {
        CreateConfiguration<LayerPropertiesProxy>()
            .HasPrimitive(source => source.Name, options => options.HasName("Наименование"));
    }
}

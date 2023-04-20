using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Configuration;

/// <summary>
/// Builds property object configuration.
/// </summary>
public class PropertyObjectConfigurationBuilder<SELF, TPropertyModel> where SELF : PropertyObjectConfigurationBuilder<SELF, TPropertyModel>
{
    /// <summary>
    /// Property object configuration.
    /// </summary>
    protected readonly PropertyObjectConfiguration Configuration = new(typeof(TPropertyModel));

    /// <summary>
    /// Set of property configurations.
    /// </summary>
    protected readonly List<IPropertyConfiguration> PropertyConfigurations = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public PropertyObjectConfigurationBuilder()
    {
        PropertyConfigurations = new();
        Configuration = new(typeof(TPropertyModel))
        {
            PropertyConfigurations = PropertyConfigurations
        };
    }

    /// <summary>
    /// Build property object configuration.
    /// </summary>
    /// <returns></returns>
    public IPropertyObjectConfiguration Build() => Configuration;
}
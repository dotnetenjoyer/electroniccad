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
    /// Set of custom section configuration.
    /// </summary>
    protected readonly List<CustomSectionConfiguration> CustomSectionConfigurations = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public PropertyObjectConfigurationBuilder()
    {
        PropertyConfigurations = new();
        Configuration = new(typeof(TPropertyModel))
        {
            PropertyConfigurations = PropertyConfigurations,
            CustomSectionConfigurations = CustomSectionConfigurations,
        };
    }

    /// <summary>
    /// Build property object configuration.
    /// </summary>
    /// <returns></returns>
    public IPropertyObjectConfiguration Build() => Configuration;

    /// <summary>
    /// Add custom section configuration.
    /// </summary>
    /// <typeparam name="T">Type of custom section.</typeparam>
    /// <param name="factoryCreator">Factory creator.</param>
    /// <returns></returns>
    public SELF HasCustomSection<T>() where T : ICustomSection
    {
        CustomSectionConfigurations.Add(new CustomSectionConfiguration<T>());
        return (SELF)this;
    }
}
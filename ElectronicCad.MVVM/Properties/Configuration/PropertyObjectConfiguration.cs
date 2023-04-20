using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Configuration;

/// <summary>
/// Describe property object configuration.
/// </summary>
public class PropertyObjectConfiguration : IPropertyObjectConfiguration
{
    /// <summary>
    /// Property model type.
    /// </summary>
    public Type SourceType { get; set; }

    /// <summary>
    /// Property configurations.
    /// </summary>
    public IEnumerable<IPropertyConfiguration> PropertyConfigurations { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="propertyModelType">Property model type</param>
    public PropertyObjectConfiguration(Type propertyModelType)
    {
        SourceType = propertyModelType;
    }
}

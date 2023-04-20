using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

namespace ElectronicCad.MVVM.Properties.Configuration;

/// <summary>
/// Property profile.
/// </summary>
public class PropertyObjectProfile
{
    /// <summary>
    /// Set of property object configurations.
    /// </summary>
    public IEnumerable<IPropertyObjectConfiguration> PropertyObjectConfigurations => objectConfigurations;

    private readonly List<IPropertyObjectConfiguration> objectConfigurations = new();

    /// <summary>
    /// Add new property object configuration.
    /// </summary>
    /// <returns></returns>
    public ConfigurationBuilder<TPropertyModel> CreateConfiguration<TPropertyModel>() where TPropertyModel : IPropertyModel
    {
        var builder = new ConfigurationBuilder<TPropertyModel>();
        
        var configuration = builder.Build();
        objectConfigurations.Add(configuration);
        
        return builder;
    }

    public class ConfigurationBuilder<TPropertyModel> : PrimitivePropertyObjectConfigurationBuilder<ConfigurationBuilder<TPropertyModel>, TPropertyModel>
    {
    }
}
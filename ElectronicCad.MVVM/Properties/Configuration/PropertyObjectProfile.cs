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
    public ConfigurationBuilder<TPropertiesProxy> CreateConfiguration<TPropertiesProxy>() where TPropertiesProxy : IPropertiesProxy
    {
        var builder = new ConfigurationBuilder<TPropertiesProxy>();
        
        var configuration = builder.Build();
        objectConfigurations.Add(configuration);
        
        return builder;
    }

    public class ConfigurationBuilder<TPropertiesProxy> : PrimitivePropertyObjectConfigurationBuilder<ConfigurationBuilder<TPropertiesProxy>, TPropertiesProxy>
    {
    }
}
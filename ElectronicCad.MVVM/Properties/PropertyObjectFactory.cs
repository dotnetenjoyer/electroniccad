using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Configuration;
using System.Reflection;

namespace ElectronicCad.MVVM.Properties;

/// <summary>
/// Property object factory.
/// </summary>
public static class PropertyObjectFactory
{
    private readonly static IEnumerable<IPropertyObjectConfiguration> propertyObjectsConfigurations;

    /// <summary>
    /// Constructor.
    /// </summary>
    static PropertyObjectFactory()
    {
        propertyObjectsConfigurations = FindAllPropertyObjectConfigurations();
    }

    private static IEnumerable<IPropertyObjectConfiguration> FindAllPropertyObjectConfigurations()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var baseProfile = typeof(PropertyObjectProfile);
        var profiles = assembly.GetTypes().Where(type => baseProfile.IsAssignableFrom(type));

        var objectConfigurations = new List<IPropertyObjectConfiguration>();
        foreach (var profile in profiles)
        {
            var instance = Activator.CreateInstance(profile) 
                ?? throw new InvalidOperationException($"Cannot create property object profile - {profile.Name}");
            var profileInstance = (PropertyObjectProfile)instance;
            objectConfigurations.AddRange(profileInstance.PropertyObjectConfigurations);
        }

        return objectConfigurations;
    }

    /// <summary>
    /// Creates property object.
    /// </summary>
    /// <returns></returns>
    public static PropertyObject Create<TProxy>(TProxy proxy) where TProxy : IProxy
    {
        var configuration = GetConfiguration(proxy);
        
        var properties = new List<IProperty>();
        foreach (var propertyConfiguration in configuration.PropertyConfigurations)
        {
            var property = PropertyFactory.Create(propertyConfiguration, proxy);
            properties.Add(property);
        }

        var propertyObject = new PropertyObject()
        {
            Properties = properties
        };
        
        return propertyObject;
    }

    private static IPropertyObjectConfiguration GetConfiguration(object source)
    {
        var configuration = propertyObjectsConfigurations
            .FirstOrDefault(c => c.SourceType == source.GetType());

        if (configuration == null)
        {
            throw new ArgumentException($"Cannot create property object for type - {source.GetType()}", nameof(source));
        }

        return configuration;
    }
}

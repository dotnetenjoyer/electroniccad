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

        var propertyObject = new PropertyObject()
        {
            Groups = CreatePropertyGroups(configuration, proxy)
        };
        
        return propertyObject;
    }

    private static IPropertyObjectConfiguration GetConfiguration(IProxy proxy)
    {
        var configuration = propertyObjectsConfigurations
            .FirstOrDefault(c => c.SourceType == proxy.GetType());

        if (configuration == null)
        {
            throw new ArgumentException($"Cannot create property object for type - {proxy.GetType()}", nameof(proxy));
        }

        return configuration;
    }

    private static IEnumerable<PropertyGroup> CreatePropertyGroups(IPropertyObjectConfiguration configuration, IProxy proxy) 
    {
        var properties = new List<IProperty>();
        foreach (var propertyConfiguration in configuration.PropertyConfigurations)
        {
            var property = PropertyFactory.Create(propertyConfiguration, proxy);
            properties.Add(property);
        }

        var groups = properties
            .GroupBy(property => property.GroupName)
            .Select(properties => new PropertyGroup
                {
                    Name = properties.Key,
                    Properties = properties
                });

        return groups;
    }
}

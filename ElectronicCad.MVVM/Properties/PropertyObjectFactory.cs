using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

namespace ElectronicCad.MVVM.Properties;

/// <summary>
/// Property object factory.
/// </summary>
public static class PropertyObjectFactory
{
    private static IEnumerable<PropertyObjectConfiguration> propertyObjectsConfigurations;

    /// <summary>
    /// Constructor.
    /// </summary>
    static PropertyObjectFactory()
    {
        
    }

    /// <summary>
    /// Creates property object.
    /// </summary>
    /// <returns></returns>
    public static PropertyObject Create(object source)
    {
        var configuration = propertyObjectsConfigurations
            .FirstOrDefault(c => c.SourceType == source.GetType());

        if(configuration == null)
        {
            return null;
        }

        var properties = new List<IProperty>();

        foreach (var propertyConfiguration in configuration.PropertyConfigurations)
        {
            var propty = new PrimitiveProperty()
            {
                Name = propertyConfiguration.Name
            };
            properties.Add(propty);
        }
        var propertyObject = new PropertyObject();
        return propertyObject;

        //propertyObjectsConfiguration.
        //var propertyObject = configuration.Create(source);

        //var properties = new List<IProperty>();
        //properties.Add(new PrimitiveProperty { Name = "First" });
        //properties.Add(new PrimitiveProperty { Name = "Second" });
        //properties.Add(new PrimitiveProperty { Name = "Third" });
        //properties.Add(new PrimitiveProperty { Name = "Fourth" });

        //var propertyObject = new PropertyObject() { Properties = properties };
        //return propertyObject;
    }
}

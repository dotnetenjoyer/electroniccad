using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

namespace ElectronicCad.MVVM.Properties.Implementation;

/// <summary>
/// Factory to create proprty based on property configuration.
/// </summary>
public static class PropertyFactory
{
    /// <summary>
    /// Creates property object based on <see cref="IPropertyConfiguration"/>.
    /// </summary>
    /// <param name="configuration">Property configuration.</param>
    /// <param name="proxy">Source object.</param>
    /// <returns></returns>
    public static IProperty Create(IPropertyConfiguration configuration, IPropertiesProxy proxy)
    {
        if (configuration is PrimitivePropertyConfiguration primitiveConfiguration)
        {
            return CreatePrimitiveProperty(primitiveConfiguration, proxy);
        }

        throw new NotSupportedException();
    }

    private static IProperty CreatePrimitiveProperty(PrimitivePropertyConfiguration primitiveConfiguration, IPropertiesProxy proxy)
    {
        var propertyType = primitiveConfiguration.SourceProperty.PropertyType;
        var propertyName = primitiveConfiguration.Name;

        var primitivePropertyType = typeof(PrimitiveProperty<>).MakeGenericType(propertyType);
        var property = (IProperty)Activator.CreateInstance(primitivePropertyType, proxy,
            primitiveConfiguration.SourceProperty, propertyName)!;

        return property;
    }
}

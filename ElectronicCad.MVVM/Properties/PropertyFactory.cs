using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

namespace ElectronicCad.MVVM.Properties;

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
    public static IProperty Create(IPropertyConfiguration configuration, IProxy proxy)
    {
        if (configuration is PrimitivePropertyConfiguration primitiveConfiguration)
        {
            var propertyType = primitiveConfiguration.SourceProperty.PropertyType;
            var propertyName = primitiveConfiguration.SourceProperty.Name;

            var primitivePropertyType = typeof(PrimitiveProperty<>).MakeGenericType(propertyType);
            var property = (IProperty)Activator.CreateInstance(primitivePropertyType, proxy, primitiveConfiguration.SourceProperty);
            property.Name = propertyName;

            return property;
        }

        throw new NotSupportedException();
        //var property = Activator.CreateInstance(primitivePropertyType, proxy, primitiveConfiguration.SourceProperty);
        //return (IProperty)property;
    }
}

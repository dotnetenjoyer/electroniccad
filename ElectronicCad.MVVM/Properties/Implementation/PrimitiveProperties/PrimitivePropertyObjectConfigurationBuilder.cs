using System.Linq.Expressions;
using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.Properties.Helpers;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Builds property object configuration with primitive properties.
/// </summary>
public class PrimitivePropertyObjectConfigurationBuilder<SELF, TPropertiesProxy> : PropertyObjectConfigurationBuilder<SELF, TPropertiesProxy>
    where SELF : PrimitivePropertyObjectConfigurationBuilder<SELF, TPropertiesProxy>
{
    /// <summary>
    /// Adds primitive property to configuration.
    /// </summary>
    /// <param name="propertySelector">Property selector.</param>
    /// <param name="primitiveOptionsBuilder">Primitive optiosn builder..</param>
    public SELF HasPrimitive<TValue>(Expression<Func<TPropertiesProxy, TValue>> propertySelector, Action<PrimitivePropertyOptionsBuilder>? primitiveOptionsBuilder = null)
    {
        var property = ReflectionHelper.FindPropertyInfo(propertySelector);

        var configuration = new PrimitivePropertyConfiguration() 
        { 
            Name = property.Name,
            SourceProperty = property,
        };

        if (primitiveOptionsBuilder != null)
        {
            var optionsBuilder = new PrimitivePropertyOptionsBuilder();
            primitiveOptionsBuilder?.Invoke(optionsBuilder);
            var options = optionsBuilder.Build();

            configuration.Name = options.Name ?? property.Name;
        }

        PropertyConfigurations.Add(configuration);
        
        return (SELF)this;
    }
}

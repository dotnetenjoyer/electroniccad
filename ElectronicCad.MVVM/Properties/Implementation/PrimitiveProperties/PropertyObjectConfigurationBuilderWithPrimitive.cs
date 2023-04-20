using ElectronicCad.MVVM.Properties.Configuration;
using System.Reflection;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Extension of property objct configuration builder with primitive adding methods.
/// </summary>
//public class PropertyObjectConfigurationBuilderWithPrimitive<TPropertyModel> : PropertyObjectConfigurationBuilder<TPropertyModel>
//{
//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    /// <param name="propertyObjectConfiguration">Property object configuration.</param>
//    public PropertyObjectConfigurationBuilderWithPrimitive(PropertyObjectConfiguration configuration) : base(configuration) 
//    {
//        ObjectConfiguration = configuration;
//    }

//    public PropertyObjectConfigurationBuilderWithPrimitive HasPrimitive(Func<typeof(TPropertyModel), PropertyInfo> source)
//    {
//        return this;
//    }
//}
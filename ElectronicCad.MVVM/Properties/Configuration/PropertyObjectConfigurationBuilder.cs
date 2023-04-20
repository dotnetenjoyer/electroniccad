using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

namespace ElectronicCad.MVVM.Properties.Configuration;

/// <summary>
/// Builds property object configuration.
/// </summary>
public class PropertyObjectConfigurationBuilder<TPropertyModel> where TPropertyModel : IPropertyModel
{
    protected readonly List<IPropertyConfiguration> PropertyConfigurations = new();

    /// <summary>
    /// Builder with ability to add primitive properties.
    /// </summary>
    //public PropertyObjectConfigurationBuilderWithPrimitive<TPropertyModel> Primitives => new();

    /// <summary>
    /// Build property object configuration.
    /// </summary>
    /// <returns></returns>
    //public IPropertyObjectConfiguration Build() => ObjectConfiguration;
}
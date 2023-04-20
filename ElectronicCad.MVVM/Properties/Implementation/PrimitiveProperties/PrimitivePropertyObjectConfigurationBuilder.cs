using ElectronicCad.MVVM.Properties.Configuration;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Builds property object configuration with primitive properties.
/// </summary>
public class PrimitivePropertyObjectConfigurationBuilder<SELF, TPropertyModel> : PropertyObjectConfigurationBuilder<SELF, TPropertyModel>
    where SELF : PrimitivePropertyObjectConfigurationBuilder<SELF, TPropertyModel>
{
    /// <summary>
    /// Adds primitive property to configuration.
    /// </summary>
    /// <param name="name">Name of primitive property.</param>
    public SELF HasPrimitive(string name)
    {
        PropertyConfigurations.Add(new PrimitivePropertyConfiguration() { Name = name });
        return (SELF)this;
    }
}
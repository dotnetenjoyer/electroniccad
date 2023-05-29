namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Properties factory.
/// </summary>
public interface IPropertyFactory
{
    /// <summary>
    /// Creates property by configuration and releated model.
    /// </summary>
    /// <param name="propertyConfiguration">Property configuration.</param>
    /// <param name="propertiesProxy">Properties proxy.</param>
    /// <returns>Property.</returns>
    IProperty Create(IPropertyConfiguration propertyConfiguration, IPropertiesProxy propertiesProxy);
}
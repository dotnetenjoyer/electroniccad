using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections;

/// <summary>
/// Factory to create custom section factories.
/// </summary>
public interface ICustomSectionFactoriesFactory
{
    /// <summary>
    /// Creates a custom section factory to a specified section.
    /// </summary>
    /// <param name="customSectionType">Type of custom section. </param>
    /// <returns>Custom section factory.</returns>
    public ICustomSectionFactory CreateFactory(Type customSectionType);
}
using ElectronicCad.MVVM.Properties.Configuration;

namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Describe property object configuraiton.
/// </summary>
public interface IPropertyObjectConfiguration
{
    /// <summary>
    /// Source type.
    /// </summary>
    Type SourceType { get; }

    /// <summary>
    /// Property configurations.
    /// </summary>
    IEnumerable<IPropertyConfiguration> PropertyConfigurations { get; }

    /// <summary>
    /// Custom section configurations.
    /// </summary>
    IEnumerable<CustomSectionConfiguration> CustomSectionConfigurations { get; }
}

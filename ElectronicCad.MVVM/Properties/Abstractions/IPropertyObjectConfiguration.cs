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
}

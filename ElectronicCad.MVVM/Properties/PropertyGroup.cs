using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties;

/// <summary>
/// Group of properties.
/// </summary>
public class PropertyGroup
{
    /// <summary>
    /// Property group name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Set of properties.
    /// </summary>
    public IEnumerable<IProperty> Properties { get; init; }
}
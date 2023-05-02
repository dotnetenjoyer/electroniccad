using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties;

/// <summary>
/// Contains properties of object.
/// </summary>
public class PropertyObject
{
    /// <summary>
    /// Properties groups.
    /// </summary>
    public IEnumerable<PropertyGroup> Groups { get; set; }

    /// <summary>
    /// Collection of custom sections.
    /// </summary>
    public IEnumerable<ICustomSection> CustomSections { get; set; }
}
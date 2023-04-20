namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Property configuration interface.
/// </summary>
public interface IPropertyConfiguration
{
    /// <summary>
    /// Property name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Property group name.
    /// </summary>
    string GroupName { get; }
}
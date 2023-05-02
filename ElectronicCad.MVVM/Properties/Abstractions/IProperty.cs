namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Property.
/// </summary>
public interface IProperty
{
    /// <summary>
    /// Name of property.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Name of group.
    /// </summary>
    string GroupName { get; }

    /// <summary>
    /// Indicates if property read only.
    /// </summary>
    bool IsReadOnly { get; }
}
namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Property.
/// </summary>
public interface IProperty
{
    /// <summary>
    /// Property name.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Indicates if property read only.
    /// </summary>
    bool IsReadOnly { get; }
}
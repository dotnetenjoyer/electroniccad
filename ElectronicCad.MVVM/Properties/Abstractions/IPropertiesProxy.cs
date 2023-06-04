
namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Properties proxy abstraction.
/// </summary>
public interface IPropertiesProxy
{
    /// <summary>
    /// Raise when the proxy is updated from the source.
    /// </summary>
    event EventHandler<EventArgs> Updated;

    /// <summary>
    /// Updates itself from its source.
    /// </summary>
    void UpdateFromSource();

    /// <summary>
    /// Updates the source.
    /// </summary>
    void UpdateSource();
}
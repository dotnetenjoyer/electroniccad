
namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Property proxy abstraction.
/// </summary>
public interface IProxy
{
    /// <summary>
    /// Raise when the proxy object is updated.
    /// </summary>
    event EventHandler<EventArgs> Updated;

    /// <summary>
    /// Updates itself from its source entity.
    /// </summary>
    void UpdateFromEntity();

    /// <summary>
    /// Updates the source entity.
    /// </summary>
    void UpdateEntity();
}

/// <summary>
/// 
/// </summary>
public interface IPropertyModel
{

}
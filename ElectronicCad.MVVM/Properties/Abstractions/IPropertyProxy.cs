
namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Property proxy abstraction.
/// </summary>
public interface IProxy
{
    /// <summary>
    /// Raise when proxy object updates.
    /// </summary>
    event EventHandler<EventArgs> Updated;

    /// <summary>
    /// Updates self from source entity.
    /// </summary>
    void UpdateFromEntity();

    /// <summary>
    /// Updates source entity.
    /// </summary>
    void UpdateEntity();
}

/// <summary>
/// 
/// </summary>
public interface IPropertyModel
{

}
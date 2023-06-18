using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ElectronicCad.Domain.Common;

public class DomainObservableObject : INotifyPropertyChanged
{
    /// <summary>
    /// The event fires when property changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Invoke <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName"></param>
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Set new value to property. 
    /// </summary>
    /// <typeparam name="T">Type of property.</typeparam>
    /// <param name="value">Target property..</param>
    /// <param name="newValue">New value.</param>
    /// <returns>True, if a new value has been set.</returns>
    public virtual bool SetProperty<T>(ref T value, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(value, newValue))
        {   
            return false;
        }

        value = newValue;
        OnPropertyChanged(propertyName);
        return true;
    }
}
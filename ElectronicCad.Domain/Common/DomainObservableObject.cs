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
}
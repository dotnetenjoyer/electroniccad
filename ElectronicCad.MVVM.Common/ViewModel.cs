using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ElectronicCad.MVVM.Common;

/// <summary>
/// Base view model.
/// </summary>
public class ViewModel : ObservableObject
{
    /// <summary>
    /// Indicates whether the view model is loaded.
    /// </summary>
    public bool IsBusy { get; set; }
    
    /// <summary>
    /// Load the state of the view model.
    /// </summary>
    public virtual Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}
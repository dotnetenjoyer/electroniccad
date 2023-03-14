using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ElectronicCad.MVVM.Common;

/// <summary>
/// Base view model.
/// </summary>
public class ViewModel : ObservableObject, IDisposable
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

    ~ViewModel()
    {
        Dispose(false);
    }
    
    protected bool IsDisposed;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Implementation disposable pattern.
    /// </summary>
    protected virtual void Dispose(bool isDisposing)
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;
    }
}
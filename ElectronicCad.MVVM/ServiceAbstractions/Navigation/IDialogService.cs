using ElectronicCad.MVVM.Common;

namespace ElectronicCad.MVVM.ServiceAbstractions.Navigation;

/// <summary>
/// Dialog service interface.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Open view.
    /// </summary>
    Task OpenAsync<TViewModel>(params object[] parameters) where TViewModel : ViewModel;

    /// <summary>
    /// Close current view.
    /// </summary>
    void Close();
}
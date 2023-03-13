using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.MVVM.ViewModels;

/// <summary>
/// Welcome dialog view model.
/// </summary>
public class WelcomeViewModel : ViewModel
{
    private readonly IDialogService _dialogService;

    /// <summary>
    /// Close dialog command.
    /// </summary>
    public RelayCommand CloseCommand
    {
        get => _closeCommand;
        set
        {
            _closeCommand = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand _closeCommand;
    

    /// <summary>
    /// Constructor.
    /// </summary>
    public WelcomeViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        CloseCommand = new RelayCommand(CloseWelcomeDialog);
    }

    private void CloseWelcomeDialog()
    {
        _dialogService.Close();
    }
}
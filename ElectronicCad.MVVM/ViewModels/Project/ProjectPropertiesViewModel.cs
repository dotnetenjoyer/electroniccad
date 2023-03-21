using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.MVVM.ViewModels.Project;

/// <summary>
/// Project properties dialog view model.
/// </summary>
public class ProjectPropertiesViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    
    /// <summary>
    /// Dialog title.
    /// </summary>
    public string Title => "Project";

    /// <summary>
    /// Close dialog command.
    /// </summary>
    public RelayCommand CloseCommand { get; set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectPropertiesViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        CloseCommand = new RelayCommand(CloseDialog);
    }

    private void CloseDialog()
    {
        _dialogService.Close();
    }
}
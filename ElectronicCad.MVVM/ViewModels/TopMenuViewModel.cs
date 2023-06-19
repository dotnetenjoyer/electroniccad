using Microsoft.Toolkit.Mvvm.Input;
using MediatR;
using ElectronicCad.UseCases.Projects.SaveProject;
using ElectronicCad.MVVM.Common;

namespace ElectronicCad.MVVM.ViewModels;

/// <summary>
/// Top menu view model.
/// </summary>
public class TopMenuViewModel : ViewModel
{
    private readonly IMediator mediator;
    
    /// <summary>
    /// Command to save project.
    /// </summary>
    public RelayCommand SaveProjectCommand { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator"></param>
    public TopMenuViewModel(IMediator mediator)
    {
        this.mediator = mediator;

        SaveProjectCommand = new RelayCommand(SaveProject);
    }

    private void SaveProject()
    {
        mediator.Send(new SaveProjectCommand());
    }
}

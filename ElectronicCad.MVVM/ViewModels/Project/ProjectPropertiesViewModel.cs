using AutoMapper;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using ElectronicCad.MVVM.ViewModels.Projects.Models;
using ElectronicCad.UseCases.Projects.CreateProject;
using ElectronicCad.UseCases.Projects.UpdateProject;
using MediatR;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.MVVM.ViewModels.Projects;

/// <summary>
/// Project properties dialog view model.
/// </summary>
public class ProjectPropertiesViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Dialog title.
    /// </summary>
    public string Title => "Project";

    /// <summary>
    /// Close dialog command.
    /// </summary>
    public RelayCommand CloseCommand { get; }

    /// <summary>
    /// Save project properties command.
    /// </summary>
    public RelayCommand SaveCommand { get; }

    /// <summary>
    /// Project properties model.
    /// </summary>
    public ProjectPropertiesModel Model { get; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectPropertiesViewModel(IDialogService dialogService, IMediator mediator, IMapper mapper)
    {
        _dialogService = dialogService;
        _mediator = mediator;
        _mapper = mapper;

        Model = new ProjectPropertiesModel();
        CloseCommand = new RelayCommand(CloseDialog);
        SaveCommand = new RelayCommand(SaveProjectProperties);
    }

    private void CloseDialog()
    {
        _dialogService.Close();
    }

    private void SaveProjectProperties()
    {
        if(Model.Id == Guid.Empty)
        {
            CreateProject();
        }
        else
        {
            UpdateProject();
        }
    }

    private void CreateProject()
    {
        var command = _mapper.Map<CreateProjectCommand>(Model);
        _mediator.Send(command);
    }

    private void UpdateProject()
    {
        var command = _mapper.Map<UpdateProjectCommand>(Model);
        _mediator.Send(command);
    }
}
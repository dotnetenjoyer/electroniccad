using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using ElectronicCad.MVVM.Utils;
using ElectronicCad.MVVM.ViewModels.Project;
using ElectronicCad.UseCases.Projects.CreateNewProject;
using MediatR;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.MVVM.ViewModels.Welcome;

/// <summary>
/// Welcome dialog view model.
/// </summary>
public class WelcomeViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly IMediator _mediator;
    private readonly ViewModelFactory _viewModelFactory;

    /// <summary>
    /// Close dialog command.
    /// </summary>
    public RelayCommand CloseCommand
    {
        get => _closeCommand;
        private set
        {
            _closeCommand = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand _closeCommand;

    /// <summary>
    /// Open project command.
    /// </summary>
    public RelayCommand OpenProjectCommand
    {
        get => _openProjectCommand;
        private set
        {
            _openProjectCommand = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand _openProjectCommand;

    /// <summary>
    /// Create porject command.
    /// </summary>
    public RelayCommand CreateProjectCommand
    {
        get => _createProjectCommand;
        private set
        {
            _createProjectCommand = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand _createProjectCommand;

    /// <summary>
    /// Recent projects view model.
    /// </summary>
    public RecentProjectsViewModel RecentProjects
    {
        get => _recentProjects;
        set
        {
            _recentProjects = value;
            OnPropertyChanged();
        }
    }

    private RecentProjectsViewModel _recentProjects;

    /// <summary>
    /// Constructor.
    /// </summary>
    public WelcomeViewModel(IDialogService dialogService, IMediator mediator, ViewModelFactory viewModelFactory)
    {
        _dialogService = dialogService;
        _mediator = mediator;
        _viewModelFactory = viewModelFactory;

        RecentProjects = _viewModelFactory.Create<RecentProjectsViewModel>();

        CloseCommand = new RelayCommand(CloseWelcomeDialog);
        CreateProjectCommand = new RelayCommand(CreateProject);
        OpenProjectCommand = new RelayCommand(OpenProject);
    }

    private void CloseWelcomeDialog()
    {
        _dialogService.Close();
    }

    private void CreateProject()
    {
        _mediator.Send(new CreateNewProjectCommand());
        _dialogService.OpenAsync<ProjectPropertiesViewModel>();
    }

    private void OpenProject()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override async Task LoadAsync()
    {
        await RecentProjects.LoadAsync();
    }
}
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using ElectronicCad.MVVM.Utils;
using ElectronicCad.MVVM.ViewModels.Projects;
using ElectronicCad.UseCases.Projects.CreateProject;
using MediatR;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.MVVM.ViewModels.Welcome;

/// <summary>
/// Welcome dialog view model.
/// </summary>
public class WelcomeViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly ViewModelFactory _viewModelFactory;
    private readonly IMediator _mediator;

    /// <summary>
    /// Close dialog command.
    /// </summary>
    public RelayCommand CloseCommand { get; }

    /// <summary>
    /// Open project command.
    /// </summary>
    public RelayCommand OpenProjectCommand { get; }

    /// <summary>
    /// Create project command.
    /// </summary>
    public RelayCommand CreateProjectCommand { get; }

    /// <summary>
    /// Recent projects view model.
    /// </summary>
    public RecentProjectsViewModel RecentProjects { get; }
    
    /// <summary>
    /// Templates view model.
    /// </summary>
    public TemplatesViewModel Templates { get; }

    /// <summary>
    /// Tips view model.
    /// </summary>    
    public TipsViewModel Tips { get; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public WelcomeViewModel(IDialogService dialogService, IMediator mediator, ViewModelFactory viewModelFactory)
    {
        _dialogService = dialogService;
        _mediator = mediator;
        _viewModelFactory = viewModelFactory;

        RecentProjects = _viewModelFactory.Create<RecentProjectsViewModel>();
        Templates = _viewModelFactory.Create<TemplatesViewModel>();
        Tips = _viewModelFactory.Create<TipsViewModel>();
            
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
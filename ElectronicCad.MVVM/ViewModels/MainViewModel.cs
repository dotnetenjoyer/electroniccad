using ElectronicCad.Domain.Geometry;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.Utils;
using ElectronicCad.MVVM.ViewModels.ActivityBar;

namespace ElectronicCad.MVVM.ViewModels;

/// <summary>
/// Main window view model.
/// </summary>
public class MainViewModel : ViewModel
{
    private readonly ICurrentProjectProvider projectProvider;
    private readonly ViewModelFactory viewModelFactory;

    /// <summary>
    /// Domain diagram.
    /// </summary>
    public Diagram Diagram
    {
        get => diagram;
        set => SetProperty(ref diagram, value);
    }

    private Diagram diagram;

    public ActivityBarViewModel ActivityBar
    {
        get => activityBar;
        set => SetProperty(ref activityBar, value);
    }

    private ActivityBarViewModel activityBar;

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainViewModel(ICurrentProjectProvider projectProvider, ViewModelFactory viewModelFactory)
    {
        this.projectProvider = projectProvider;
        this.viewModelFactory = viewModelFactory;

        var currentProject = projectProvider.GetCurrentProject(); 
        Diagram = currentProject.Diagrams.First().GeometryDiagram;

        ActivityBar = viewModelFactory.Create<ActivityBarViewModel>();
    }

    /// <inheritdoc />
    public override Task LoadAsync()
    {
        return activityBar.LoadAsync();
    }
}
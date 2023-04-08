using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.Utils;

namespace ElectronicCad.MVVM.ViewModels.ActivityBar;

/// <summary>
/// Activity bar view model.
/// </summary>
public class ActivityBarViewModel : ViewModel
{
    private readonly ViewModelFactory viewModelFactory;

    /// <summary>
    /// Project diagrams view model.
    /// </summary>
    public ProjectDiagramsViewModel ProjectDiagrams 
    { 
        get => diagrams; 
        private set => SetProperty(ref diagrams, value); 
    }

    private ProjectDiagramsViewModel diagrams;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ActivityBarViewModel(ViewModelFactory viewModelFactory)
    {
        this.viewModelFactory = viewModelFactory;
        ProjectDiagrams = viewModelFactory.Create<ProjectDiagramsViewModel>();
    }

    public override Task LoadAsync()
    {
        return ProjectDiagrams.LoadAsync();
    }
}

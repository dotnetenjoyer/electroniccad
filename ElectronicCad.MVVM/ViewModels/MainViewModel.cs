using ElectronicCad.Domain.Geometry;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.Utils;
using ElectronicCad.MVVM.ViewModels.ActivityBar;
using ElectronicCad.MVVM.ViewModels.Diagrams;
using ElectronicCad.MVVM.ViewModels.Properties;

namespace ElectronicCad.MVVM.ViewModels;

/// <summary>
/// Main window view model.
/// </summary>
public class MainViewModel : ViewModel
{
    private readonly ViewModelFactory viewModelFactory;

    /// Activity bar view model.
    /// </summary>
    public ActivityBarViewModel ActivityBar
    {
        get => activityBar;
        set => SetProperty(ref activityBar, value);
    }

    private ActivityBarViewModel activityBar;

    /// <summary>
    /// Diagram view model.
    /// </summary>
    public DiagramViewModel Diagram
    {
        get => diagramControl;
        set => SetProperty(ref diagramControl, value);
    }

    private DiagramViewModel diagramControl;

    /// <summary>
    /// Property view model.
    /// </summary>
    public PropertyViewModel Property
    {
        get => property;
        set => SetProperty(ref property, value);
    }

    private PropertyViewModel property;

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainViewModel(ViewModelFactory viewModelFactory)
    {
        this.viewModelFactory = viewModelFactory;

        ActivityBar = viewModelFactory.Create<ActivityBarViewModel>();
        Property = viewModelFactory.Create<PropertyViewModel>();
        Diagram = viewModelFactory.Create<DiagramViewModel>();
    }

    /// <inheritdoc />
    public override Task LoadAsync()
    {
        return activityBar.LoadAsync();
    }
}
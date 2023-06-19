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

    /// <summary>
    /// Top menu view model
    /// </summary>
    public TopMenuViewModel TopMenu { get; }


    /// Activity bar view model.
    /// </summary>
    public ActivityBarViewModel ActivityBar { get; }

    /// <summary>
    /// Diagram view model.
    /// </summary>
    public DiagramViewModel Diagram { get; }

    /// <summary>
    /// Property view model.
    /// </summary>
    public PropertyViewModel Property { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainViewModel(ViewModelFactory viewModelFactory)
    {
        this.viewModelFactory = viewModelFactory;
        
        TopMenu = viewModelFactory.Create<TopMenuViewModel>();
        ActivityBar = viewModelFactory.Create<ActivityBarViewModel>();
        Property = viewModelFactory.Create<PropertyViewModel>();
        Diagram = viewModelFactory.Create<DiagramViewModel>();
    }

    /// <inheritdoc />
    public override Task LoadAsync()
    {
        var loadTopMenu = TopMenu.LoadAsync();
        var loadActivityBar = ActivityBar.LoadAsync();
        var loadProperty = Property.LoadAsync();
        var loadDiagram = Diagram.LoadAsync();

        return Task.WhenAll(loadTopMenu, loadActivityBar, loadProperty, loadDiagram);
    }
}
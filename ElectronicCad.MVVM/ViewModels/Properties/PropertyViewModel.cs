using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.Properties;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties;

/// <summary>
/// Property view model.
/// </summary>
public class PropertyViewModel : ViewModel
{
    private readonly ISelectionService selectionService;

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);

    }

    private string name;

    /// <summary>
    /// Selected object properties.
    /// </summary>
    public PropertyObject PropertyObject
    {
        get => propertyObject;
        set => SetProperty(ref propertyObject, value);
    }

    private PropertyObject propertyObject;

    /// <summary>
    /// Constructor.
    /// </summary>
    public PropertyViewModel(ISelectionService selectionService)
    {
        this.selectionService = selectionService;

        selectionService.SelectionChanged += SelectionService_SelectionChanged;
    }

    private void SelectionService_SelectionChanged(object? sender, EventArgs e)
    {
        var selectedObject = selectionService.SelectedObjects.First();

        var proxy = ProxyFactory.Create(selectedObject);
        PropertyObject = PropertyObjectFactory.Create(proxy);
    }
}
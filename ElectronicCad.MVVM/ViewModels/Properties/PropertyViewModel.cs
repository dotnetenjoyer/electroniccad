using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.Properties;
using ElectronicCad.MVVM.Properties.Implementation;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties;

/// <summary>
/// Property view model.
/// </summary>
public class PropertyViewModel : ViewModel
{
    private readonly ISelectionService selectionService;
    private readonly PropertyObjectFactory propertyObjectFactory;

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
    public PropertyViewModel(ISelectionService selectionService, PropertyObjectFactory propertyObjectFactory)
    {
        this.selectionService = selectionService;
        this.propertyObjectFactory = propertyObjectFactory;

        selectionService.SelectionChanged += SelectionService_SelectionChanged;
    }

    private void SelectionService_SelectionChanged(object? sender, EventArgs e)
    {
        if (selectionService.SelectedObjects.Any())
        {
            var proxy = ProxyFactory.Create(selectionService.SelectedObjects.First());
            PropertyObject = propertyObjectFactory.Create(proxy);
        }
    }
}
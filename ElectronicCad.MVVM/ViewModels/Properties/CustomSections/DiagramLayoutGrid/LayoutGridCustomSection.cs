using System.Collections.ObjectModel;
using AutoMapper;
using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

/// <summary>
/// Diagram layout grid custom section.
/// </summary>
public sealed class LayoutGridCustomSection : ICustomSection
{
    private readonly ILayoutGridProxy proxy;
    private readonly IMapper mapper;

    /// <summary>
    /// Custom section view model
    /// </summary>
    public LayoutGridsModel Model { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy"></param>
    public LayoutGridCustomSection(ILayoutGridProxy proxy, IMapper mapper)
    {
        this.proxy = proxy;
        this.mapper = mapper;

        Model = new();
        Model.LayoutGridAdded += HandleLayoutGridAdd;
        Model.LayoutGridUpdated += HandleLayoutGridUpdate;
        Model.LayoutGridRemoved += HandleLayoutGridRemove;

        proxy.Updated += HandleProxyUpdate;
        UpdateFromProxy();
    }

    private void HandleLayoutGridAdd(object? sender, LayoutGridModel layoutGridModel)
    {
        var layoutGrid = mapper.Map<LayoutGrid>(layoutGridModel);
        proxy.AddLayoutGrid(layoutGrid);
    }

    private void HandleLayoutGridUpdate(object? sender, LayoutGridModel updatedLayoutGridModel)
    {
        var layoutGrid = mapper.Map<LayoutGrid>(updatedLayoutGridModel);
        proxy.UpdateLayoutGrid(layoutGrid);
    }

    private void HandleLayoutGridRemove(object? sender, LayoutGridModel layoutGridModel)
    {
        var layoutGrid = proxy.LayoutGrids.First(x => x.Id == layoutGridModel.Id);
        proxy.RemoveLayoutGrid(layoutGrid);
    }

    private void HandleProxyUpdate(object? sender, EventArgs e)
    {
        UpdateFromProxy();
    }
    
    private void UpdateFromProxy()
    {
        var layoutGridModels = mapper.Map<IEnumerable<LayoutGridModel>>(proxy.LayoutGrids);
        Model.LayoutGrids = new ObservableCollection<LayoutGridModel>(layoutGridModels);
    }
}
 
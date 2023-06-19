using System.Collections.ObjectModel;
using AutoMapper;
using ElectronicCad.Domain.Geometry.Layouts;
using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout;

/// <summary>
/// Diagram layout custom section.
/// </summary>
public sealed class LayoutCustomSection : ICustomSection
{
    private readonly ILayoutProxy proxy;
    private readonly IMapper mapper;

    /// <summary>
    /// Custom section view model
    /// </summary>
    public LayoutsModel Model { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy"></param>
    public LayoutCustomSection(ILayoutProxy proxy, IMapper mapper)
    {
        this.proxy = proxy;
        this.mapper = mapper;

        Model = new();
        Model.LayoutAdded += HandleLayoutGridAdd;
        Model.LayoutGridUpdated += HandleLayoutUpdate;
        Model.LayoutRemoved += HandleLayoutGridRemove;

        proxy.Updated += HandleProxyUpdate;
        UpdateFromProxy();
    }

    private void HandleLayoutGridAdd(object? sender, LayoutModel layoutModel)
    {
        var layout = mapper.Map<Layout>(layoutModel);
        proxy.AddLayout(layout);
    }

    private void HandleLayoutUpdate(object? sender, LayoutModel layoutModel)
    {
        var layout = mapper.Map<Layout>(layoutModel);
        proxy.UpdateLayout(layout);
    }

    private void HandleLayoutGridRemove(object? sender, LayoutModel layoutModel)
    {
        var layout = proxy.Layouts.First(x => x.Id == layoutModel.Id);
        proxy.RemoveLayout(layout);
    }

    private void HandleProxyUpdate(object? sender, EventArgs e)
    {
        UpdateFromProxy();
    }
    
    private void UpdateFromProxy()
    {
        var layoutModels = mapper.Map<IEnumerable<LayoutModel>>(proxy.Layouts);
        Model.Layouts = new ObservableCollection<LayoutModel>(layoutModels);
    }
}
 
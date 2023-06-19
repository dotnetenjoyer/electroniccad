using Microsoft.Toolkit.Mvvm.Input;
using MediatR;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Common;
using ElectronicCad.UseCases.DiagramsTrees.Dtos;
using ElectronicCad.UseCases.DiagramsTrees.GetDiagramsTree;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;
using ElectronicCad.MVVM.ViewModels.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ElectronicCad.MVVM.ViewModels.ActivityBar;

/// <summary>
/// View model to diagrams activity bar section.
/// </summary>
public class ProjectDiagramsViewModel : ViewModel
{
    private readonly IOpenProjectProvider openProjectProvider;
    private readonly IMediator mediator;
    private readonly ISelectionService selectionService;
    private readonly DiagramsContextMenuFactory contextMenuFactory;

    /// <summary>
    /// Project diagram trees.
    /// </summary>
    public DiagramTrees? DiagramTrees 
    { 
        get => diagramTrees;
        set => SetProperty(ref diagramTrees, value); 
    }

    private DiagramTrees? diagramTrees;

    public IEnumerable<TreeNode> SelectedNodes => selectedItems
        .OfType<TreeNode>()  
        .ToList(); 

    /// <summary>
    /// Selected diagram items.
    /// </summary>
    public IEnumerable<object> SelectedItems
    {
        get => selectedItems;
        set => SetProperty(ref selectedItems, value);
    }

    private IEnumerable<object> selectedItems = Array.Empty<object>();

    /// <summary>
    /// Command to open context menu.
    /// </summary>
    public RelayCommand ContextMenuOpeningCommand { get; }

    /// <summary>
    /// Collection of context menu commands.
    /// </summary>
    public IEnumerable<ContextMenuCommand> ContextMenuCommands 
    { 
        get => contextMenuCommands; 
        set => SetProperty(ref contextMenuCommands, value); 
    }

    private IEnumerable<ContextMenuCommand> contextMenuCommands = Array.Empty<ContextMenuCommand>();

    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectDiagramsViewModel(IOpenProjectProvider openProjectProvider, IMediator mediator,
        ISelectionService selectionService, DiagramsContextMenuFactory contextMenuFactory)
    {
        this.openProjectProvider = openProjectProvider;
        this.mediator = mediator;
        this.contextMenuFactory = contextMenuFactory;

        this.selectionService = selectionService;
        this.selectionService.SelectionChanged += HandleSelectionServiceSelectedItemsChange;

        ContextMenuOpeningCommand = new RelayCommand(HandleContextMenuOpening);

        PropertyChanged += HandlePropertyChange;
    }

    #region Sync with selection service.

    private void HandlePropertyChange(object? sender, System.ComponentModel.PropertyChangedEventArgs eventArgs)
    {
        if (eventArgs.PropertyName == nameof(SelectedItems) && !isSyncsWithSelectionService)
        {
            var selectedItems = SelectedNodes.Select(x => x.NodeObject).ToArray();
            selectionService.Select(selectedItems);
        }
    }


    private bool isSyncsWithSelectionService;

    private void HandleSelectionServiceSelectedItemsChange(object? sender, EventArgs eventArgs)
    {
        isSyncsWithSelectionService = true;

        var nodes = GetFlatDiagramTree();
        SelectedItems = nodes
            .Where(n => selectionService.SelectedObjects.Contains(n.NodeObject))
            .ToList();

        isSyncsWithSelectionService = false;
    }

    private IEnumerable<TreeNode> GetFlatDiagramTree()
    {
        if (DiagramTrees == null || !DiagramTrees.Diagrams.Any())
        {
            return Array.Empty<TreeNode>();
        }

        return DiagramTrees.Diagrams.SelectMany(diagram => GetAllNodes(diagram));

        IEnumerable<TreeNode> GetAllNodes(TreeNode node)
        {
            if (node.Nodes == null || !node.Nodes.Any())
            {
                return new TreeNode[] { node };
            }

            var nodes = node.Nodes
                .SelectMany(n => GetAllNodes(n))
                .ToList();

            nodes.Insert(0, node);

            return nodes;
        }
    }

    #endregion
    
    private void HandleContextMenuOpening()
    {
        var selectedItems = SelectedNodes.Select(x => x.NodeObject).ToList();
        if (selectedItems.Any())
        {
            ContextMenuCommands = contextMenuFactory.CreateContextMenu(selectedItems);
        }
    }

    /// <inheritdoc />
    public override async Task LoadAsync()
    {
        var currentProject = openProjectProvider.GetOpenProject();

        foreach (var projectDiagram in currentProject.Diagrams)
        {
            projectDiagram.GeometryDiagram.GeometryAdded += HandleGeometyUpdate;
            projectDiagram.GeometryDiagram.GeometryRemoved += HandleGeometyUpdate;
            projectDiagram.GeometryDiagram.LayerAdded += HandleLayersUpdate;
            projectDiagram.GeometryDiagram.LayerRemoved += HandleLayersUpdate;
        }

        await UpdateDiagramTrees();
    }

    private async void HandleLayersUpdate(object? sender, Layer layer)
    {
        await UpdateDiagramTrees();
    }

    private async void HandleGeometyUpdate(object? sender, IEnumerable<GeometryObject> geometry)
    {
        await UpdateDiagramTrees();
    }
    
    private async Task UpdateDiagramTrees()
    {
        // TODO: can be optimized.
        var diagramTrees = await mediator.Send(new GetProjectDiagramTreesQuery());
        DiagramTrees = diagramTrees;
    }
}

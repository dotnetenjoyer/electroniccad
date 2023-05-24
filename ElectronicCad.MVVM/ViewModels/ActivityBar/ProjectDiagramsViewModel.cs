using Microsoft.Toolkit.Mvvm.Input;
using MediatR;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Common;
using ElectronicCad.UseCases.DiagramsTrees.Dtos;
using ElectronicCad.UseCases.DiagramsTrees.GetDiagramsTree;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;
using ElectronicCad.MVVM.ViewModels.Common;

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

    /// <summary>
    /// Selected node.
    /// </summary>
    public DiagramTreeNode SelectedNode
    {
        get => selectedNode;
        set
        {
            var selectedObjects = new[] { value.DomainObject };
            selectionService.Select(selectedObjects);
            SetProperty(ref selectedNode, value);
        }
    }

    private DiagramTreeNode selectedNode;

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
        this.selectionService = selectionService;
        this.contextMenuFactory = contextMenuFactory;

        ContextMenuOpeningCommand = new RelayCommand(HandleContextMenuOpening);
    }
    
    private void HandleContextMenuOpening()
    {
        if (SelectedNode != null)
        {
            var contextMenuCommands = contextMenuFactory.CreateContextMenu(new object[] { SelectedNode.DomainObject });
            ContextMenuCommands = contextMenuCommands;
        }
    }

    /// <inheritdoc />
    public override async Task LoadAsync()
    {
        var currentProject = openProjectProvider.GetOpenProject();

        foreach (var projectDiagram in currentProject.Diagrams)
        {
            projectDiagram.GeometryDiagram.GeometryAdded += HandleGeometryAdd;
            projectDiagram.GeometryDiagram.GeometryRemoved += HandleGeometryRemove;
        }

        await UpdateDiagramTrees();
    }

    private async void HandleGeometryAdd(object? sender, GeometryObject geometry)
    {
        await UpdateDiagramTrees();
    }

    private async void HandleGeometryRemove(object? sender, GeometryObject geometry)
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

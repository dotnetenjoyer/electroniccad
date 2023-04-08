using MediatR;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.MVVM.Common;
using ElectronicCad.UseCases.DiagramsTrees.Dtos;
using ElectronicCad.UseCases.DiagramsTrees.GetDiagramsTree;

namespace ElectronicCad.MVVM.ViewModels.ActivityBar;

/// <summary>
/// View model to diagrams activity bar section.
/// </summary>
public class ProjectDiagramsViewModel : ViewModel
{
    private readonly ICurrentProjectProvider projectProvider;
    private readonly IMediator mediator;

    /// <summary>
    /// Project diagram trees.
    /// </summary>
    public DiagramTrees DiagramTrees 
    { 
        get => diagramTrees;
        set => SetProperty(ref diagramTrees, value); 
    }

    private DiagramTrees diagramTrees;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectDiagramsViewModel(ICurrentProjectProvider projectProvider, IMediator mediator)
    {
        this.projectProvider = projectProvider;
        this.mediator = mediator;
    }

    /// <inheritdoc />
    public override async Task LoadAsync()
    {
        var currentProject = projectProvider.GetCurrentProject();

        foreach (var projectDiagram in currentProject.Diagrams)
        {
            projectDiagram.GeometryDiagram.GeometryAdded += HandleGeometryAdd;
            projectDiagram.GeometryDiagram.GeometryRemoved += HandleGeometryRemove;
        }

        await UpdateDiagramTrees();
    }

    private void HandleGeometryAdd(object? sender, GeometryObject geometry)
    {
        UpdateDiagramTrees();
    }

    private void HandleGeometryRemove(object? sender, GeometryObject geometry)
    {
        UpdateDiagramTrees();
    }

    private async Task UpdateDiagramTrees()
    {
        // TODO: can be optimized.
        var diagramTrees = await mediator.Send(new GetProjectDiagramTreesQuery());
        DiagramTrees = diagramTrees;
    }
}

using MediatR;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.UseCases.DiagramsTrees.Dtos;
using ElectronicCad.Domain.Workspace;

namespace ElectronicCad.UseCases.DiagramsTrees.GetDiagramsTree;

/// <summary>
/// Handler for <see cref="GetProjectDiagramTreesQuery"/>
/// </summary>
public class GetProjectDiagramTreesQueryHandler : IRequestHandler<GetProjectDiagramTreesQuery, DiagramTrees>
{
    private readonly ICurrentProjectProvider projectProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetProjectDiagramTreesQueryHandler(ICurrentProjectProvider projectProvider)
    {
        this.projectProvider = projectProvider;
    }

    /// <inheritdoc />
    public async Task<DiagramTrees> Handle(GetProjectDiagramTreesQuery request, CancellationToken cancellationToken)
    {
        var project = projectProvider.GetCurrentProject();
        var projectDiagrams = project.Diagrams.Where(d => d is ProjectDiagram);

        var diagramNodes = new List<DiagramTreeNode>();
        foreach (var projectDiagram in projectDiagrams)
        {
            var geometryObjects = projectDiagram.GeometryDiagram.GeometryObjects;
            var diagramNode = new WorkspaceDiagramDiagramTreeNode(projectDiagram)
            {
                Nodes = geometryObjects.Select(g => new GeometryDiagramTreeNode(g))
            };
            
            diagramNodes.Add(diagramNode);
        }
        
        var diagramTree = new DiagramTrees()
        {
            Diagrams = diagramNodes
        };

        return diagramTree;
    }
}

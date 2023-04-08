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

        var diagramTree = new DiagramTrees();
        var diagrams = new List<DiagramTreeNode>();

        var projectDiagrams = project.Diagrams.Where(x => x is ProjectDiagram);
        foreach (var projectDiagram in projectDiagrams)
        {
            var diagramNodes = projectDiagram.GeometryDiagram.GeometryObjects.Select(geometryObject => new DiagramTreeNode
            {
                DomainObject = geometryObject
            });

            var diagram = new DiagramTreeNode
            {
                DomainObject = projectDiagram,
                Nodes = diagramNodes
            };
            diagrams.Add(diagram);
        }
        diagramTree.Diagrams = diagrams;

        return diagramTree;
    }
}
